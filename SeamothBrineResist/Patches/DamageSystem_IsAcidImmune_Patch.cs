using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace SeamothBrineResist.Patches
{
    [HarmonyPatch(typeof(DamageSystem), nameof(DamageSystem.IsAcidImmune))]
    public static class DamageSystem_IsAcidImmune_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref GameObject go, ref bool __result)
        {
            TechType techType = CraftData.GetTechType(go);
            // make a variable and store the Damage.acidImmune content into a List
            var acidImmune = DamageSystem.acidImmune == null ? new List<TechType>() : DamageSystem.acidImmune.ToList();
            // if the list doesn't have TechType.Seamoth
            if (!acidImmune.Contains(TechType.Seamoth))
                // Add TechType.Seamoth to the list
                acidImmune.Add(TechType.Seamoth);
            if (techType != TechType.None)
            {
                for (int i = 0; i < DamageSystem.acidImmune.Length; i++)
                {
                    if (techType == DamageSystem.acidImmune[i])
                    {
                        __result = true;
                        return false;
                    }
                    DamageSystem.acidImmune = acidImmune.ToArray(); // Adding our list content into the original Array
                }
            }
            __result = false;
            return true;
        }
    }
}
