using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ExosuitPickupperPropulsionCannon.MonoBehaviours;
using HarmonyLib;
using QModManager.Utility;

namespace ExosuitPickupperPropulsionCannon.Patches
{
    [HarmonyPatch(typeof(Exosuit))]
    public class ExosuitPatcher
    {
        [HarmonyPatch(nameof(Exosuit.UpdateUIText))]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new(instructions);
            
            var pickupText = AccessTools.Method(typeof(ExosuitPatcher), nameof(PickupText));

            bool found = false;
            
            for (int i = 0; i < codes.Count(); i++)
            {
                if (codes[i].opcode == OpCodes.Ldstr && (string)codes[i].operand == "PropulsionCannonToRelease")
                {
                    codes.Insert(i + 5, new CodeInstruction(OpCodes.Ldarg_0));
                    codes.Insert(i + 6, new CodeInstruction(OpCodes.Call, pickupText));
                    
                    found = true;
                    break;
                }
            }
            
            if (found is true)
                Logger.Log(Logger.Level.Debug, "Transpiler Succeeded!");
            else
                Logger.Log(Logger.Level.Error, "Transpiler Failed.");

            return codes.AsEnumerable();
        }

        static void PickupText(Exosuit exosuit)
        {
            string buttonText = uGUI.FormatButton(GameInput.Button.Deconstruct);
            exosuit.sb.AppendLine($"Pickup Grabbed item ({buttonText})");
        }
    }
}