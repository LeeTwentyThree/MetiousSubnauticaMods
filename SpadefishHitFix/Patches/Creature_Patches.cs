using HarmonyLib;
using UnityEngine;

namespace SpadefishHitFix.Patches
{
    [HarmonyPatch(typeof(Creature))]
    public class Creature_Patches
    {
        [HarmonyPatch(nameof(Creature.Start))]
        [HarmonyPostfix]
        static void StartPostfix(Creature __instance)
        {
            var techType = CraftData.GetTechType(__instance.gameObject);

            if (techType == TechType.Spadefish)
                __instance.gameObject.GetComponent<Rigidbody>().mass = 2.5f;
        }
    }
}