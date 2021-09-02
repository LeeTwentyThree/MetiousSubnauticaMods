using QModManager.API.ModLoading;
using System.Reflection;
using System.IO;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using Sprite = Atlas.Sprite;
using HarmonyLib;
using static LootDistributionData;
using System.Collections.Generic;
using UWE;
namespace RockGrubPickupable
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        [QModPatch]
        public static void Load()
        {
            InventoryAndWaterParkSetup();
            Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
        }
        #region Inventory and WaterPark Setup
        private static void InventoryAndWaterParkSetup()
        {
            // Setting a name for the Rockgrub
            LanguageHandler.SetTechTypeName(TechType.Rockgrub, "Rockgrub");
            // Setting a Tooltip for the Rockgrub
            LanguageHandler.SetTechTypeTooltip(TechType.Rockgrub, "A small, luminescent scavenger");

            // Setting a Sprite for the Rockgrub
            Sprite rockgrub = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "RockGrub.png"));
            if (rockgrub != null)
            {
                SpriteHandler.RegisterSprite(TechType.Rockgrub, rockgrub);
            }
            // Setting Rockgrub's size in the Inventory
            CraftDataHandler.SetItemSize(TechType.Rockgrub, new Vector2int(1, 1));
            // Setting WPC Parameters for Rockgrub so it can grow and breed normaly
            WaterParkCreature.waterParkCreatureParameters[TechType.Rockgrub] = new WaterParkCreatureParameters(0.03f, 0.7f, 1f, 1f);
            // Setting Fuel value for the Rockgrub
            BioReactorHandler.SetBioReactorCharge(TechType.Rockgrub, 350f);
            // Totally origina.. *cough* taken from MrPurple's CYS code
            #region BiomeData stuff
            Dictionary<TechType, List<BiomeData>> rockgrubBiomeData = new Dictionary<TechType, List<BiomeData>>()
            {
                {
                    TechType.Rockgrub,
                    new List<BiomeData>()
                    {
                        new BiomeData()
                        {
                            biome = BiomeType.SafeShallows_CaveSpecial,
                            count = 3,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_CaveCeiling,
                            count = 3,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_CaveRecess,
                            count = 5,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_CaveSpecial,
                            count = 3,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_GiantTreeInteriorRecess,
                            count = 3,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_GiantTreeInteriorSpecial,
                            count = 3,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.KooshZone_CaveWall,
                            count = 3,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SparseReef_DeepWall,
                            count = 4,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_CaveCeiling,
                            count = 3,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.Dunes_Rock,
                            count = 4,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.Dunes_SandPlateau,
                            count = 4,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.Dunes_CaveCeiling,
                            count = 4,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.Dunes_CaveWall,
                            count = 4,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.Mountains_CaveWall,
                            count = 4,
                            probability = 1f
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_CaveFloor,
                            count = 4,
                            probability = 1f
                        },
                    }
                }
            };
            foreach (KeyValuePair<TechType, List<BiomeData>> pair in rockgrubBiomeData)
            {
                string classId = CraftData.GetClassIdForTechType(pair.Key) ?? pair.Key.AsString();
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
                            techType = pair.Key
                        };
                    }
                    WorldEntityDatabaseHandler.AddCustomInfo(classId, info);
                }
                SrcData data = new SrcData() { prefabPath = prefabpath, distribution = pair.Value };
                LootDistributionHandler.AddLootDistributionData(classId, data);
            }
            #endregion
        }
        #endregion
    }
}
