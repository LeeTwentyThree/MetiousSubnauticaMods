using HarmonyLib;
using ColorizableSpotlight.MonoBehaviours;
namespace ColorizableSpotlight.Patches
{
    [HarmonyPatch(typeof(BaseSpotLight), nameof(BaseSpotLight.Start))]
    public static class BaseSpotlight_Start_Patch
    {
        public static void Postfix(BaseSpotLight __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);
            if (techType == TechType.Spotlight)
            {
                __instance.gameObject.EnsureComponent<ColoringController>();
            }
        }
    }
}
