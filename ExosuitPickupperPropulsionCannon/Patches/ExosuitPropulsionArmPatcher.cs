using ExosuitPickupperPropulsionCannon.MonoBehaviours;
using HarmonyLib;

namespace ExosuitPickupperPropulsionCannon.Patches
{
    [HarmonyPatch(typeof(ExosuitPropulsionArm))]
    public class ExosuitPropulsionArmPatcher
    {
        [HarmonyPatch(nameof(ExosuitPropulsionArm.Start))]
        [HarmonyPostfix]
        static void Postfix(ExosuitPropulsionArm __instance) => __instance.gameObject.EnsureComponent<PickupperPropulsionCannon>();
    }
}