using HarmonyLib;

namespace AdditionalCyclopsLightController.Patches
{
    [HarmonyPatch(typeof(CyclopsLightingPanel))]
    internal static class CyclopsLightingPanelPatches
    {
        [HarmonyPatch(nameof(CyclopsLightingPanel.ToggleInternalLighting))]
        [HarmonyPatch(nameof(CyclopsLightingPanel.ToggleFloodlights))]
        [HarmonyPostfix]
        private static void ToggleLightsPostfix(CyclopsLightingPanel __instance)
        {
            UpdateAllLightControllers(__instance);
        }

        // To globally sync all of the light controller in the current cyclops
        private static void UpdateAllLightControllers(CyclopsLightingPanel controller)
        {
            var lightSource = controller.floodlightsHolder.transform.GetChild(0).gameObject; // reads the first floodlight
            var lightingPanels = controller.cyclopsRoot.GetComponentsInChildren<CyclopsLightingPanel>();
            foreach (var lightingPanel in lightingPanels)
            {
                lightingPanel.lightingOn = controller.cyclopsRoot.subLightsOn;
                lightingPanel.floodlightsOn = lightSource.activeSelf; // checks if the floodlight is active
                lightingPanel.UpdateLightingButtons(); // to sync the buttons images with the new lightingOn and floodlightOn values
            }
        }
    }
}