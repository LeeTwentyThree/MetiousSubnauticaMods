namespace ColorizableSpotlight.Patches
{
    using HarmonyLib;
    using MonoBehaviours;
    
    [HarmonyPatch(typeof(BaseSpotLight), nameof(BaseSpotLight.Start))]
    public static class BaseSpotlight_Start_Patch
    {
        public static void Postfix(BaseSpotLight __instance)
        {
            var techType = CraftData.GetTechType(__instance.gameObject);
            if (techType == TechType.Spotlight)
            {
                var cc = __instance.gameObject.EnsureComponent<ColoringController>();
                cc.enabled = __instance.constructed;
            }
        }
    }
}
