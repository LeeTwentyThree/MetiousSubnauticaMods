namespace ExosuitSprint

open System.Diagnostics
open System.Runtime.CompilerServices
open System.Collections.Generic
open SMLHelper.V2.Assets
open SMLHelper.V2.Patchers
open SMLHelper.V2.Patchers.EnumPatching

[<Extension>]
type Extensions =
    
    [<Extension>]
    static member Unpatch(equipable: Equipable) =
        if equipable.IsPatched then
            ModPrefab.ClassIdDictionary.Remove(equipable.ClassID) |> ignore
            ModPrefab.FileNameDictionary.Remove(equipable.PrefabFileName) |> ignore
            ModPrefab.PreFabsList.Remove(equipable) |> ignore
            
            let mutable dick: Dictionary<string, Atlas.Sprite> = null 
            if ModSprite.ModSprites.TryGetValue(SpriteManager.Group.None, &dick) then
                dick.Remove(equipable.TechType.AsString()) |> ignore
            
            LootDistributionPatcher.CustomSrcData.Remove(equipable.ClassID); |> ignore
            WorldEntityDatabasePatcher.CustomWorldEntityInfos.Remove(equipable.ClassID) |> ignore
            let withoutThisTT =
                CraftTreePatcher.CraftingNodes 
                |> Seq.toList 
                |> List.filter (fun c -> not(int c.TechType = int equipable.TechType)) 
                |> ResizeArray
                
            CraftTreePatcher.CraftingNodes <- withoutThisTT
            if uGUI.isMainLevel && not uGUI.isLoading then
                CraftTree.initialized <- false
                Language.main.strings.["Tooltip_" + equipable.ClassID] <- "unpatched. Does nothing."
            
            if TechTypePatcher.cacheManager.entriesFromRequests.IsKnownKey(equipable.TechType) then
                TechTypePatcher.cacheManager.entriesFromRequests.Remove(equipable.TechType, int equipable.TechType, equipable.ClassID)
            
            equipable.IsPatched <- false
        

type Logging =
    [<Conditional("DEBUG")>]
    static member Log (msg: string) =
        msg |> ErrorMessage.AddDebug
        QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Debug, msg)