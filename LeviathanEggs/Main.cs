using System.Globalization;
using System.Collections.Generic;
using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using System.IO;
using System.Reflection;
using LeviathanEggs.Prefabs;
using UnityEngine;
using static LootDistributionData;
using UWE;
using LeviathanEggs.MonoBehaviours;
namespace LeviathanEggs
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        public const string version = "1.0.0.0";
        internal static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, "eggs"));
        internal static SeaEmperorEgg seaEmperorEgg = new SeaEmperorEgg();
        internal static SeaDragonEgg seaDragonEgg = new SeaDragonEgg();
        internal static GhostEgg ghostEgg = new GhostEgg();

        public static List<TechType> TechTypesToSkyApply = new List<TechType>() { TechType.SeaDragon, TechType.GhostLeviathan, TechType.GhostLeviathanJuvenile, TechType.SeaEmperorJuvenile, TechType.SeaEmperorBaby, TechType.SeaEmperor };
        public static List<TechType> TechTypesToMakePickupable = new List<TechType>() { TechType.GhostLeviathan, TechType.GhostLeviathanJuvenile, TechType.SeaDragon };
        public static List<TechType> TechTypesToTweak = new List<TechType>() { TechType.Bleeder, TechType.Rockgrub };
        [QModPatch]
        public static void Load()
        {
            seaEmperorEgg.Patch(); 
            seaDragonEgg.Patch();
            ghostEgg.Patch();

            PDAHandler.AddCustomScannerEntry(new PDAScanner.EntryData()
            {
                key = seaEmperorEgg.TechType,
                encyclopedia = "UnknownEgg",
                scanTime = 2f,
                isFragment = false
            });
            PDAHandler.AddCustomScannerEntry(new PDAScanner.EntryData()
            {
                key = seaDragonEgg.TechType,
                encyclopedia = "UnknownEgg",
                scanTime = 2f,
                isFragment = false
            });
            PDAHandler.AddCustomScannerEntry(new PDAScanner.EntryData()
            {
                key = ghostEgg.TechType,
                encyclopedia = "UnknownEgg",
                scanTime = 2f,
                isFragment = false
            });

            WaterParkCreatureParametersSettings();

            Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
        }
        private static void WaterParkCreatureParametersSettings()
        {
            #region TechTypeNames
            LanguageHandler.SetTechTypeName(TechType.SeaEmperorBaby, "Sea Emperor Baby");
            LanguageHandler.SetTechTypeName(TechType.SeaEmperorJuvenile, "Sea Emperor Juvenile");
            LanguageHandler.SetTechTypeName(TechType.SeaEmperor, "Sea Emperor");

            LanguageHandler.SetTechTypeName(TechType.SeaDragon, "Sea Dragon");

            LanguageHandler.SetTechTypeName(TechType.GhostLeviathanJuvenile, "Ghost Leviathan Juvenile");
            #endregion
            #region Tooltips
            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperorBaby, "Gigantic sentient filter-feeder, with a passive demeanor and unique healing properties, raised in containment.");
            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperorJuvenile, "Gigantic sentient filter-feeder, with a passive demeanor and unique healing properties, raised in containment.");
            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperor, "Gigantic sentient filter-feeder, with a passive demeanor and unique healing properties, raised in containment.");

            LanguageHandler.SetTechTypeTooltip(TechType.SeaDragon, "Colossal, territorial Leviathan with reptilian features, raised in containment.");

            
            LanguageHandler.SetTechTypeTooltip(TechType.GhostLeviathanJuvenile, "Enormous, aggressive, eel-like apex predator, raised in containment.");
            LanguageHandler.SetTechTypeTooltip(TechType.GhostLeviathan, "Enormous, aggressive, eel-like apex predator, raised in containment.");
            #endregion
            #region Sprites
            SpriteHandler.RegisterSprite(TechType.SeaEmperorBaby, ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "SeaEmperor.png")));
            SpriteHandler.RegisterSprite(TechType.SeaEmperorJuvenile, ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "SeaEmperor.png")));
            SpriteHandler.RegisterSprite(TechType.SeaEmperor, ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "SeaEmperor.png")));

            SpriteHandler.RegisterSprite(TechType.SeaDragon, ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "SeaDragon.png")));

            SpriteHandler.RegisterSprite(TechType.GhostLeviathan, ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "Ghost.png")));
            SpriteHandler.RegisterSprite(TechType.GhostLeviathanJuvenile, ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "Ghost.png")));
            #endregion
            #region ItemSizes
            CraftDataHandler.SetItemSize(TechType.SeaEmperorBaby, new Vector2int(4, 4));
            CraftDataHandler.SetItemSize(TechType.SeaEmperorJuvenile, new Vector2int(4, 4));
            CraftDataHandler.SetItemSize(TechType.SeaEmperor, new Vector2int(4, 4));

            CraftDataHandler.SetItemSize(TechType.SeaDragon, new Vector2int(4, 4));

            CraftDataHandler.SetItemSize(TechType.GhostLeviathanJuvenile, new Vector2int(4, 4));
            CraftDataHandler.SetItemSize(TechType.GhostLeviathan, new Vector2int(4, 4));
            #endregion
            #region WaterParkCreatureParameters
            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperor] = new WaterParkCreatureParameters(0.03f, 0.04f, 0.07f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperorJuvenile] = new WaterParkCreatureParameters(0.03f, 0.04f, 0.07f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperorBaby] = new WaterParkCreatureParameters(0.03f, 0.04f, 0.07f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.SeaDragon] = new WaterParkCreatureParameters(0.03f, 0.04f, 0.07f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.GhostLeviathanJuvenile] = new WaterParkCreatureParameters(0.03f, 0.05f, 0.07f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.GhostLeviathan] = new WaterParkCreatureParameters(0.03f, 0.05f, 0.07f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.Bleeder] = new WaterParkCreatureParameters(0.2f, 0.7f, 1f, 1f, true);
            #endregion
            #region Creature Eggs
            WaterParkCreature.creatureEggs[TechType.GhostLeviathanJuvenile] = ghostEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.SeaDragon] = seaDragonEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.SeaEmperorJuvenile] = seaEmperorEgg.TechType;

            WaterParkCreature.creatureEggs.Remove(TechType.Spadefish);
            #endregion
            #region Loot Distributon
            #region Reefback Egg Spawns
            Dictionary<TechType, List<BiomeData>> reefbackEggBiomes = new Dictionary<TechType, List<BiomeData>>()
            {
                {
                    TechType.ReefbackEgg,
                    new List<BiomeData>()
                    {
                        new BiomeData()
                        {
                            biome = BiomeType.KooshZone_CaveFloor,
                            probability = 0.08f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.KooshZone_Mountains,
                            probability = 0.08f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.KooshZone_Sand,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_Grass,
                            probability = 0.09f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_Sand,
                            probability = 0.02f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_Grass,
                            probability = 0.05f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_GiantTreeExterior,
                            probability = 0.02f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SparseReef_Sand,
                            probability = 0.04f,
                            count = 1,
                        }
                    }
                }
            };
            foreach (KeyValuePair<TechType, List<BiomeData>> pair in reefbackEggBiomes)
            {
                string reefbackEggClassId = CraftData.GetClassIdForTechType(TechType.ReefbackEgg);
                if (PrefabDatabase.TryGetPrefabFilename(reefbackEggClassId, out string prefabpath))
                {
                    if (!WorldEntityDatabase.TryGetInfo(reefbackEggClassId, out WorldEntityInfo info))
                    {
                        info = new WorldEntityInfo()
                        {
                            cellLevel = LargeWorldEntity.CellLevel.Medium,
                            classId = reefbackEggClassId,
                            prefabZUp = false,
                            slotType = EntitySlot.Type.Medium,
                            techType = pair.Key,
                            localScale = Vector3.one
                        };
                    }
                    WorldEntityDatabaseHandler.AddCustomInfo(reefbackEggClassId, info);
                }
                SrcData data = new SrcData() { distribution = pair.Value, prefabPath = prefabpath };
                LootDistributionHandler.AddLootDistributionData(reefbackEggClassId, data);
            }
            #endregion
            #region Jumper Egg Spawns
            Dictionary<TechType, List<BiomeData>> jumperEggBiomes = new Dictionary<TechType, List<BiomeData>>()
            {
                {
                    TechType.JumperEgg,
                    new List<BiomeData>()
                    {
                        new BiomeData()
                        {
                            biome = BiomeType.SparseReef_DeepFloor,
                            probability = 0.5f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SparseReef_CaveFloor,
                            probability = 0.5f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.Dunes_CaveFloor,
                            probability = 0.1f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_CaveFloor,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_CaveFloor,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_CaveSpecial,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SafeShallows_CaveFloor,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SafeShallows_CaveSpecial,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SeaTreaderPath_CaveFloor,
                            probability = 0.01f,
                            count = 1,
                        },
                    }
                }
            };
            foreach (KeyValuePair<TechType, List<BiomeData>> kvp in jumperEggBiomes)
            {
                string jumperEggClassId = CraftData.GetClassIdForTechType(TechType.JumperEgg);
                if (PrefabDatabase.TryGetPrefabFilename(jumperEggClassId, out string prefabpath))
                {
                    if (!WorldEntityDatabase.TryGetInfo(jumperEggClassId, out WorldEntityInfo info))
                    {
                        info = new WorldEntityInfo()
                        {
                            cellLevel = LargeWorldEntity.CellLevel.Medium,
                            classId = jumperEggClassId,
                            prefabZUp = false,
                            slotType = EntitySlot.Type.Medium,
                            techType = kvp.Key,
                            localScale = Vector3.one
                        };
                    }
                    WorldEntityDatabaseHandler.AddCustomInfo(jumperEggClassId, info);
                }
                SrcData data = new SrcData() { distribution = kvp.Value, prefabPath = prefabpath };
                LootDistributionHandler.AddLootDistributionData(jumperEggClassId, data);

            }
            #endregion
            #region Spadefish Egg Removal
            string spadeFishEggClassId = CraftData.GetClassIdForTechType(TechType.SpadefishEgg);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.SparseReef_DeepCoral, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.GrassyPlateaus_Grass, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.GrassyPlateaus_CaveFloor, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.UnderwaterIslands_CaveFloor_Obsolete, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.UnderwaterIslands_ValleyLedge, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.GrandReef_Grass, 0f, 0);
            #endregion
            #endregion
        }
    }
}
