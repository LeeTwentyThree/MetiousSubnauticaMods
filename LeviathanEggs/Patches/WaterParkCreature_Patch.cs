using HarmonyLib;
using UnityEngine;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(WaterParkCreature), nameof(WaterParkCreature.Update))]
    class WaterParkCreature_Patch
    {
        [HarmonyPostfix]
        static void Postfix(WaterParkCreature __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);
            foreach (TechType tt in Main.TechTypesToSkyApply)
            {
                if (techType == tt)
                {
                    SkyApplier skyApplier = __instance.gameObject.EnsureComponent<SkyApplier>();

                    if (__instance.gameObject.TryGetComponent(out __instance) && __instance.IsInsideWaterPark())
                        skyApplier.anchorSky = Skies.BaseInterior;
                    else
                        skyApplier.anchorSky = Skies.Auto;
                    skyApplier.renderers = __instance.gameObject.GetAllComponentsInChildren<Renderer>();
                    skyApplier.dynamic = true;
                    skyApplier.emissiveFromPower = false;
                    skyApplier.hideFlags = HideFlags.None;
                    skyApplier.enabled = true;
                }
            }
        }
    }
}
