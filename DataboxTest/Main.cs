using HarmonyLib;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
namespace DataboxTest
{
    using CustomDataboxes.API;
    using QModManager.API.ModLoading;
    [QModCore]
    public static class Main
    {
        public const string version = "1.0.0.0";
        [QModPatch]
        public static void Load()
        {
            Databox databox = new Databox()
            {
                DataboxID = "StasisRifleDatabox",
                AlreadyUnlockedDescription = "StasisRifle Databox, already unlocked",
                PrimaryDescription = "StasisRifle Databox",
                SecondaryDescription = "Stasis Rifle Databox",
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
                },
                TechTypeToUnlock = TechType.StasisRifle
            };
            databox.Patch();
        }
    }
}
