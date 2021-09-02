using HarmonyLib;
using UnityEngine;
namespace RockGrubPickupable.Patches
{
    [HarmonyPatch]
    class Creature_Start_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
        private static void Postfix(Creature __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);
            if (techType == TechType.Rockgrub)
            {
                // Add Pickupable component to the original fish
                Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;
                // Add WaterParkCreature component to the original fish
                __instance.gameObject.EnsureComponent<WaterParkCreature>().pickupable = pickupable;

                // Add Eatable component to the original fish
                Eatable eatable = __instance.gameObject.EnsureComponent<Eatable>();
                eatable.foodValue = 10f;
                eatable.waterValue = 4f;

                // Clone the original fish and remove the extra Components from it
                GameObject obj = Object.Instantiate(__instance.gameObject);
                foreach (var component in obj.GetComponents<Component>())
                {
                    Object.Destroy(component);
                }
                // Add AquariumFish component to the original fish
                AquariumFish aquariumFish = __instance.gameObject.EnsureComponent<AquariumFish>();
                aquariumFish.model = obj;

                // Set the Object inactive cause yes
                obj.SetActive(false);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterParkCreature), nameof(WaterParkCreature.Start))]
        private static void StartPostfix(WaterParkCreature __instance)
        {
            if (CraftData.GetTechType(__instance.gameObject) != TechType.Rockgrub) 
                return;

            __instance.gameObject.EnsureComponent<Pickupable>().isPickupable = true;
        }
    }
}
