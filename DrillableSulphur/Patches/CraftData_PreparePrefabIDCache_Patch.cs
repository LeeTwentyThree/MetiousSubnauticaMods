using System.Collections.Generic;
using HarmonyLib;

namespace DrillableSulphur.Patches
{
    class CraftData_PreparePrefabIDCache_Patch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Dictionary<TechType, string> techMapping = CraftData.techMapping;
            Dictionary<string, TechType> entClassTechTable = CraftData.entClassTechTable;

            techMapping[TechType.DrillableSulphur] = "697beac5-e39a-4809-854d-9163da9f997e";
            entClassTechTable["697beac5-e39a-4809-854d-9163da9f997e"] = TechType.DrillableSulphur;
        }
    }
}
