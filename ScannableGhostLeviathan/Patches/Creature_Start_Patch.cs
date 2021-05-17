using HarmonyLib;
using UnityEngine;
namespace ScannableGhostLeviathan.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
    public class Creature_Start_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Creature __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);
            if (techType == TechType.GhostLeviathan)
            {
                ResourceTracker resourceTracker = __instance.gameObject.EnsureComponent<ResourceTracker>();
                resourceTracker.techType = TechType.GhostLeviathan;
                resourceTracker.overrideTechType = TechType.GhostLeviathan;
                resourceTracker.rb = __instance.gameObject.GetComponent<Rigidbody>();
                resourceTracker.prefabIdentifier = __instance.gameObject.GetComponent<PrefabIdentifier>();
                resourceTracker.pickupable = __instance.gameObject.GetComponent<Pickupable>();
            }
            else if (techType == TechType.GhostLeviathanJuvenile)
            {
                ResourceTracker resourceTracker = __instance.gameObject.EnsureComponent<ResourceTracker>();
                resourceTracker.techType = TechType.GhostLeviathanJuvenile;
                resourceTracker.overrideTechType = TechType.GhostLeviathan;
                resourceTracker.rb = __instance.gameObject.GetComponent<Rigidbody>();
                resourceTracker.prefabIdentifier = __instance.gameObject.GetComponent<PrefabIdentifier>();
                resourceTracker.pickupable = __instance.gameObject.GetComponent<Pickupable>();
            }
        }
    }
}
