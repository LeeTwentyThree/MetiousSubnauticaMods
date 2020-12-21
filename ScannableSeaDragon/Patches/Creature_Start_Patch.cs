using HarmonyLib;
using UnityEngine;
namespace ScannableSeaDragon.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
    public class Creature_Start_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Creature __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);
            if (techType == TechType.SeaDragon)
            {
                ResourceTracker resourceTracker = __instance.gameObject.EnsureComponent<ResourceTracker>();
                resourceTracker.prefabIdentifier = __instance.gameObject.GetComponent<PrefabIdentifier>();
                resourceTracker.techType = TechType.SeaDragon;
                resourceTracker.overrideTechType = TechType.SeaDragon;
                resourceTracker.rb = __instance.gameObject.GetComponent<Rigidbody>();
                resourceTracker.pickupable = __instance.gameObject.GetComponent<Pickupable>();
            }
        }
    }
}
