namespace ExosuitSprint

open System.Linq
open ExosuitSprint
open System.Reflection.Emit
open System.Text
open UnityEngine
open HarmonyLib

[<HarmonyPatch(typeof<Exosuit>)>]
module ExosuitPatches =
    type internal Type = interface end
    
    [<HarmonyPatch("Start"); HarmonyPostfix>]
    let Start (__instance: Exosuit) =
        if not QPatch.config.makeModule then __instance.gameObject.EnsureComponent<HorizontalJet>() |> ignore
        
    [<HarmonyPatch("OnUpgradeModuleChange"); HarmonyPostfix>]
    let OnUpgradeModuleChange(__instance: Exosuit, techType: TechType) =
        if techType = Main.boostModule.TechType then
            if __instance.modules.GetCount(techType) > 0 then
                __instance.gameObject.EnsureComponent<HorizontalJet>() |> ignore
            elif QPatch.config.makeModule then
                Object.Destroy(__instance.gameObject.GetComponent<HorizontalJet>())
        
    [<HarmonyPatch("OverrideAcceleration"); HarmonyPrefix>]
    let OverrideAcceleration (__instance: Exosuit, acceleration: byref<Vector3>) =
        let exists, horizontalJet = __instance.TryGetComponent<HorizontalJet>()
        if exists then
            if not __instance.onGround then
                if horizontalJet.HorizontalJetActive then
                    acceleration.x <- 0.0f
                    acceleration.z <- 0.0f
                elif horizontalJet.VerticalJetActive then
                    let speed = if __instance.jumpJetsUpgraded then 0.3f else 0.22f
                    acceleration.x <- acceleration.x * speed
                    acceleration.z <- acceleration.z * speed
                    
            false
        else
            true
              
    
    [<HarmonyPatch("UpdateUIText"); HarmonyTranspiler>]
    let UpdateUIText (instructions: seq<CodeInstruction>) =
        seq {
            for i in instructions do
                yield i
                    
                if i.Calls(AccessTools.PropertySetter(typeof<StringBuilder>, "Length")) then
                    yield CodeInstruction(OpCodes.Ldarg_0)
                    yield CodeInstruction.Call(typeof<Type>.DeclaringType, "BoostUI")
        }
        
    let BoostUI (e: Exosuit) =
        let exists, _ = e.TryGetComponent<HorizontalJet>()
        if exists then
            e.sb.AppendLine(LanguageCache.GetButtonFormat("Speed boost ({0})", GameInput.Button.Sprint))
                .AppendLine(LanguageCache.GetButtonFormat("Vertical thrust ({0})", GameInput.Button.MoveUp))
            |> ignore
        
    let rec updateFindCode (i: byref<int>, codes: ResizeArray<CodeInstruction>) =
        if codes.[i].opcode = OpCodes.Ldarg_0 && codes.[i + 1].opcode = OpCodes.Ldarg_0 &&
            codes.[i + 2].opcode = OpCodes.Ldfld && codes.[i + 3].opcode = OpCodes.Ldloc_S &&
            codes.[i + 4].opcode = OpCodes.Add && codes.[i + 5].opcode = OpCodes.Call &&
            codes.[i + 6].opcode = OpCodes.Stfld then true
        else
            i <- i + 1
            updateFindCode(&i, codes)
            
    [<HarmonyPatch("Update"); HarmonyTranspiler>]
    let Update (instructions: seq<CodeInstruction>) = 
        let mutable found = false
        let mutable i = 0
        let codes = instructions |> ResizeArray
        if updateFindCode(&i, codes) then
            codes.[i].opcode <- OpCodes.Nop
            codes.RemoveRange(i + 1, 6)
            codes.Insert(i + 1, CodeInstruction.Call(typeof<Type>.DeclaringType, "AddThrust"))
            codes.Insert(i + 1, CodeInstruction(OpCodes.Ldarg_0))
            found <- true
                    
        match found with
        | true -> "Update transpiler was successful."
        | false -> "Update transpiler failed successfully."
        |> Logging.Log
        let sb = StringBuilder()
        codes.ForEach(fun c -> sb.AppendLine(c.ToString()) |> ignore)
        sprintf "Update instructions:\n%O" sb |> Logging.Log
        codes.AsEnumerable()
        
        
                    
    let AddThrust (e: Exosuit) =
        let exists, _ = e.TryGetComponent<HorizontalJet>()
        if not exists then
            let additionAmount =
                match e.onGround with
                | true -> Time.deltaTime * e.thrustConsumption * 4.0f;
                | false -> Time.deltaTime * e.thrustConsumption * 0.7f;
            
            e.thrustPower <- Mathf.Clamp01(e.thrustPower + additionAmount);