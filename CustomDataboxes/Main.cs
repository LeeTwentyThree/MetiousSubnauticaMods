namespace CustomDataboxes
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using SMLHelper.V2.Handlers;
    using QModManager.API.ModLoading;
    using System.IO;
    using System.Reflection;
#if SN1
    using Oculus.Newtonsoft.Json;
    using Oculus.Newtonsoft.Json.Converters;
#elif BZ
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
#endif
    using Databoxes;
    using Converter;
    using SMLHelper.V2.Assets;
    using UnityEngine;

    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        private static readonly DirectoryInfo DataboxFolder = Directory.CreateDirectory(Path.Combine(ModPath, "Databoxes"));
        internal static string BiomeList = ModPath + "/Biomes.json";
        internal static string ExampleFile = ModPath + "/ExampleFile.json";
        public const string version = "1.2.0.0";

        [QModPostPatch]
        public static void Load()
        {
            EnsureBiomeList();
            EnsureExample();

            CreateDataboxesAndLoadFiles();
        }
        private static void CreateDataboxesAndLoadFiles()
        {
            foreach (FileInfo file in DataboxFolder.GetFiles().Where((x) => x.Extension.ToLower() == ".json"))
            {
                try
                {
                    DataboxInfo databox;
                    using (var reader = new StreamReader(file.FullName))
                    {
                        var serializer = new JsonSerializer();
                        databox = JsonConvert.DeserializeObject<DataboxInfo>(reader.ReadToEnd(), new JsonConverter[] 
                        {
                            new StringEnumConverter()
                            {
#if SN1
                                CamelCaseText = true,
#elif BZ
                                NamingStrategy = new CamelCaseNamingStrategy(),
#endif
                                AllowIntegerValues = true
                            },
                            new TechTypeConverter(),
                            new Vector3Converter()
                        });
                    }
                    if (databox != null)
                    {
                        var tt = TechTypeExtensions.FromString(databox.ItemToUnlock, out var techType, true) ? techType
                            : TechTypeHandler.TryGetModdedTechType(databox.ItemToUnlock, out techType) ? techType
                                : TechType.None;

                        if (tt != TechType.None)
                        {
                            var customDatabox = new DataboxPrefab(databox.DataboxID, databox.AlreadyUnlockedDescription, databox.PrimaryDescription, databox.SecondaryDescription, tt, databox.BiomesToSpawnIn, databox.CoordinatedSpawns);
                            customDatabox.Patch();
                        }
                        else
                        {
                            throw new Exception($"Couldn't parse {databox.ItemToUnlock} to TechType.");
                        }
                    }
                    else
                    {
                        QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Error, $"Unable to load Custom Databox from {Path.GetDirectoryName(file.FullName)}!");
                    }
                }
                catch (Exception e)
                {
                    QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Error, $"Unable to load Custom Databox from {Path.GetDirectoryName(file.FullName)}!", e);
                }
            }
        }
        private static void EnsureExample()
        {
            if (!File.Exists(ExampleFile))
            {
                DataboxInfo databox = new DataboxInfo()
                {
                    DataboxID = "SeaglideDatabox",
                    AlreadyUnlockedDescription = "Seaglide Databox. already unlocked",
                    PrimaryDescription = "Unlock Seaglide",
                    SecondaryDescription = "a Databox to unlock the Seaglide",
                    ItemToUnlock = TechType.Seaglide.AsString(),
                    CoordinatedSpawns = new List<Spawnable.SpawnLocation>()
                    {
                        new Spawnable.SpawnLocation(new Vector3(5, -5, 5), new Vector3(10, -100, 253))
                    },
#if SN1
                    BiomesToSpawnIn = new List<LootDistributionData.BiomeData>
                    {
                        new LootDistributionData.BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_Grass,
                            count = 1,
                            probability = 0.1f
                        },
                        new LootDistributionData.BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_TechSite,
                            count = 1,
                            probability = 0.5f
                        },
                        new LootDistributionData.BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_TechSite_Scattered,
                            count = 1,
                            probability = 0.1f
                        }
                    }
#elif BZ
                    BiomesToSpawnIn = new List<LootDistributionData.BiomeData>
                    {
                        new LootDistributionData.BiomeData()
                        {
                            biome = BiomeType.TwistyBridges_Cave_Ground,
                            count = 1,
                            probability = 0.1f
                        },
                        new LootDistributionData.BiomeData()
                        {
                            biome = BiomeType.TwistyBridges_Coral,
                            count = 1,
                            probability = 0.1f
                        },
                        new LootDistributionData.BiomeData()
                        {
                            biome = BiomeType.SparseArctic_Ground,
                            count = 1,
                            probability = 0.1f
                        }
                    }
#endif
                };
                using (StreamWriter writer = new StreamWriter(ExampleFile))
                {
                    writer.Write(JsonConvert.SerializeObject(databox, Formatting.Indented, new JsonConverter[]
                    {
                        new StringEnumConverter()
                        {
#if SN1
                            CamelCaseText = true,
#elif BZ
                            NamingStrategy = new CamelCaseNamingStrategy(),
#endif
                            AllowIntegerValues = true
                        },
                        new Vector3Converter()
                    }));
                }
            }
        }
        private static void EnsureBiomeList()
        {
            if (!File.Exists(BiomeList))
            {
                List<string> biomeList = new List<string>();
                foreach (BiomeType biome in Enum.GetValues(typeof(BiomeType)))
                {
                    biomeList.Add(biome.AsString());

                }
                using (StreamWriter writer = new StreamWriter(BiomeList))
                {
                    writer.Write(JsonConvert.SerializeObject(biomeList, Formatting.Indented));
                }
            }
        }
    }
}
