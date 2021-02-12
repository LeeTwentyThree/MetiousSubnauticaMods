using HarmonyLib;
namespace DrillableSulphur.Patches
{
    class Drillable_Start_Patch
    {
        [HarmonyPostfix]
        public static void DrillableStart(Drillable __instance)
        {
            if (__instance.gameObject.name.Contains("DrillableSulphur"))
            {
                __instance.gameObject.EnsureComponent<TechTag>().type = TechType.DrillableSulphur;
                __instance.gameObject.EnsureComponent<PrefabIdentifier>().ClassId = "697beac5-e39a-4809-854d-9163da9f997e";
                __instance.gameObject.GetComponent<SkyApplier>().enabled = true;
                ResourceTracker rt = __instance.gameObject.GetComponent<ResourceTracker>();
                if (rt != null)
                {
                    rt.overrideTechType = TechType.Sulphur;
                    rt.techType = TechType.Sulphur;
                }
            }
        }
    }
}
