using HarmonyLib;
using SeaMothAutomaticSonar.MonoBehaviours;

namespace SeaMothAutomaticSonar.Patches
{
    [HarmonyPatch(typeof(SeaMoth))]
    class SeaMoth_Patches
    {
        [HarmonyPatch(nameof(SeaMoth.OnUpgradeModuleToggle))]
        [HarmonyPrefix]
        static bool OnUpgradeModuleToggle_Prefix(SeaMoth __instance, int slotID, bool active)
        {
            if (__instance.modules.GetTechTypeInSlot(__instance.slotIDs[slotID]) == TechType.SeamothSonarModule)
            {
                var seaMothController = __instance.gameObject.EnsureComponent<SeaMothSonarController>();
                seaMothController.seaMoth = __instance;
                seaMothController.enabled = active;

                return false;
            }

            return true;
        }
    }
}