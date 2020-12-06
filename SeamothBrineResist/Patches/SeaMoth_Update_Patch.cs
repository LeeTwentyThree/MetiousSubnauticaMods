using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
namespace SeamothBrineResist.Patches
{
    [HarmonyPatch(typeof(SeaMoth), nameof(SeaMoth.Update))]
    public class SeaMoth_Update_Patch
    {
        [HarmonyPostfix]
        static void Prefix(SeaMoth __instance)
        {
            var count = __instance.modules.GetCount(Modules.SeamothBrineResistanceModule.TechTypeID); // get the Module count

            if (count > 0) // if the module is equipped
            {
                // first we convert the DamageSystem.acidImmune array to a list and store it to the acidImmune variable
                var acidImmune = DamageSystem.acidImmune == null ? new List<TechType>() : DamageSystem.acidImmune.ToList();
                // if the acidImmune doesn't have the Seamoth, add it
                if (!acidImmune.Contains(TechType.Seamoth))
                    acidImmune.Add(TechType.Seamoth);
                DamageSystem.acidImmune = acidImmune.ToArray();
            }
        }
    }
}
