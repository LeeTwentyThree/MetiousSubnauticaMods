using HarmonyLib;
using UnityEngine;
namespace RockGrubPickupable.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
    class Creature_Start_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Creature __instance)
        {
            // totally origina... *cough* stolen from MrPurple's CustomCommands code
            TechType techType = CraftData.GetTechType(__instance.gameObject);
            if (techType == TechType.Rockgrub)
            {
                Pickupable pickupable = __instance.gameObject.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                //pickupable.version = 4;

                __instance.gameObject.EnsureComponent<WaterParkCreature>().pickupable = pickupable;
                AquariumFish aquariumFish = __instance.gameObject.EnsureComponent<AquariumFish>();
                aquariumFish.model = __instance.gameObject;

                //the piece of code below makes the game in a loop  that it continues cloning the clone forever so im commenting it out
                /*GameObject gameObject = Object.Instantiate<GameObject>(aquariumFish.model, Vector3.zero, Quaternion.identity);
                AnimateByVelocity animateByVelocity = __instance.gameObject.GetComponentInChildren<AnimateByVelocity>();
                animateByVelocity.rootGameObject = gameObject;
                animateByVelocity.animationMoveMaxSpeed = 0.5f;*/
            }
        }
    }
}
