using HarmonyLib;
using UnityEngine;

namespace AdditionalCyclopsLightController.Patches
{
    [HarmonyPatch(typeof(CyclopsNoiseManager))]
    internal static class CyclopsNoiseManagerPatches
    {
        private const string kLightsControllerName = "LightsControlDownStairs";
        
        // To instantiate the down stairs light controller
        [HarmonyPatch(nameof(CyclopsNoiseManager.Start))]
        [HarmonyPostfix]
        private static void StartPostfix(CyclopsNoiseManager __instance)
        {
            if (__instance.gameObject.FindChild(kLightsControllerName) is not null) // down stairs light controller already exists, exit early
                return;
            
            var prefab = __instance.gameObject.FindChild("LightsControl"); // get the lights controller game object

            if (!prefab) // Safety check
                return;

            var downStairsLightsController = Object.Instantiate(prefab, prefab.transform.parent, prefab.transform.position, prefab.transform.rotation, true);
            downStairsLightsController.name = kLightsControllerName;
            downStairsLightsController.transform.localPosition = new Vector3(-1.09f, -5f, -19.43f); // adjust the position of the second light controller
        }
    }
}