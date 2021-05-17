using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
namespace SeamothBrineResist.Patches
{
    [HarmonyPatch(typeof(SeaMoth), nameof(SeaMoth.OnUpgradeModuleChange))]
    public class SeaMoth_Update_Patch
    {
        [HarmonyPostfix]
        static void Postfix(SeaMoth __instance)
        {
            var count = __instance.modules.GetCount(Modules.SeamothBrineResistanceModule.TechTypeID); // get the Module count
            // convert the DamageSystem.acidImmune array to a list and store it to the acidImmune variable
            var acidImmune = DamageSystem.acidImmune == null ? new List<TechType>() : DamageSystem.acidImmune.ToList();
            if (count > 0) // if the module is equipped
            {
                // add the seamoth to the acidImmune list
                if (!acidImmune.Contains(TechType.Seamoth))
                    acidImmune.Add(TechType.Seamoth);
            }
            else if (count == 0) // if the module isn't equipped
            {
                // remove the seamoth from the acidImmune list
                if (acidImmune.Contains(TechType.Seamoth))
                    acidImmune.Remove(TechType.Seamoth);               
            }
            DamageSystem.acidImmune = acidImmune.ToArray();
        }
    }
}
