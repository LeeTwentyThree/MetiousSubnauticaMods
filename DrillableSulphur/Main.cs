using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using System.Collections.Generic;
using System.Reflection;
using UWE;
using static LootDistributionData;
using DrillableSulphur.Patches;
namespace DrillableSulphur
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        public const string version = "1.0.0.0";
        [QModPatch]
        public static void Load()
        {
            Harmony harmony = new Harmony($"Metious_{myAssembly.GetName().Name}");

            // most disgusting thing i've wrote in my life. ughhh
            var prefabCacheOriginal = AccessTools.Method(typeof(CraftData), nameof(CraftData.PreparePrefabIDCache));
            var prefabCachePostfix = new HarmonyMethod(AccessTools.Method(typeof(CraftData_PreparePrefabIDCache_Patch), nameof(CraftData_PreparePrefabIDCache_Patch.Postfix)));
            harmony.Patch(prefabCacheOriginal, prefabCachePostfix);

            var drillableStartOriginal = AccessTools.Method(typeof(Drillable), nameof(Drillable.Start));
            var drillableStartPostfix = new HarmonyMethod(AccessTools.Method(typeof(Drillable_Start_Patch), nameof(Drillable_Start_Patch.DrillableStart)));
            harmony.Patch(drillableStartOriginal, drillableStartPostfix);

            DrillableSulphurSrcData();

        }
        private static void DrillableSulphurSrcData()
        {
            List<BiomeData> distribution = new List<BiomeData>()
            {
                new BiomeData()
                {
                    biome = BiomeType.BonesField_LakePit_Floor,
                    count = 1,
                    probability = 0.3f
                },
                new BiomeData()
                {
                    biome = BiomeType.BonesField_Lake_Floor,
                    count = 1,
                    probability = 0.075f
                },
                new BiomeData()
                {
                    biome = BiomeType.LostRiverJunction_LakeFloor,
                    count = 1,
                    probability = 0.05f
                },
                new BiomeData()
                {
                    biome = BiomeType.InactiveLavaZone_LavaPit_Floor,
                    count = 1,
                    probability = 0.05f
                },
                new BiomeData()
                {
                    biome = BiomeType.InactiveLavaZone_Chamber_Floor_Far,
                    count = 1,
                    probability = 0.01f
                },
                new BiomeData()
                {
                    biome = BiomeType.ActiveLavaZone_Chamber_Floor,
                    count = 1,
                    probability = 0.2f
                }
            };

            string classId = CraftData.GetClassIdForTechType(TechType.DrillableSulphur) ?? TechType.DrillableSulphur.AsString();
            if (PrefabDatabase.TryGetPrefabFilename(classId, out string prefabpath))
            {
                if (!WorldEntityDatabase.TryGetInfo(classId, out WorldEntityInfo info))
                {
                    info = new WorldEntityInfo()
                    {
                        cellLevel = LargeWorldEntity.CellLevel.Medium,
                        classId = classId,
                        localScale = UnityEngine.Vector3.one,
                        prefabZUp = false,
                        slotType = EntitySlot.Type.Medium,
                        techType = TechType.DrillableSulphur
                    };
                }
                WorldEntityDatabaseHandler.AddCustomInfo(classId, info);
            }
            SrcData data = new SrcData() { prefabPath = prefabpath, distribution = distribution };
            LootDistributionHandler.AddLootDistributionData(classId, data);
        }
    }
}
