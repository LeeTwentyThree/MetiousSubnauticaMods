using QModManager.API.ModLoading;
using System.Reflection;
using System.IO;
using CustomBatteries.API;
using SMLHelper.V2.Utility;
using System;
namespace NuclearBatteries
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");

        public const string version = "1.1.1.1";
        public const string modName = "[NuclearBatteries] ";

        [QModPatch]
        public static void Load()
        {
            CreateAndPatchPacks();
        }
        #region Create And Patch Packs
        private static void CreateAndPatchPacks()
        {
            var nBattery = new CbBattery // Calling the CustomBatteries API to patch this item as a Battery
            {
                EnergyCapacity = 2500,
                ID = "NuclearBattery",
                Name = "Ion-fused Nuclear Battery",
                FlavorText = "A Battery made by the Precursor Technology and its interaction with Nuclear Power.",
                CraftingMaterials = { TechType.UraniniteCrystal, TechType.UraniniteCrystal, TechType.PrecursorIonCrystal, TechType.Lead, TechType.Lead, TechType.Nickel },
                UnlocksWith = TechType.UraniniteCrystal,
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "NuclearBattery.png")),
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "NuclearBatteryskin.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "NuclearBatteryillum.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "NuclearBatteryspec.png")),
                    CustomIllumStrength = 0.95f,
                    UseIonModelsAsBase = true
                },
            };
            nBattery.Patch();

            var nPowercell = new CbPowerCell // Calling the CustomBatteries API to patch this item as a Power Cell
            {
                EnergyCapacity = nBattery.EnergyCapacity * 2,
                ID = "NuclearPowercell",
                Name = "Ion-fused Nuclear Power Cell",
                FlavorText = "A Power Cell made by the Precursor Technology and its interaction with Nuclear Power.",
                CraftingMaterials = { nBattery.TechType, nBattery.TechType, TechType.Silicone },
                UnlocksWith = nBattery.TechType,
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "NuclearPowerCell.png")),
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "Nuclearcellskin.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "Nuclearcellillum.png")),
                    UseIonModelsAsBase = true,
                    CustomIllumStrength = 1.2f
                },
            };
            nPowercell.Patch();

            var unBattery = new CbBattery // Calling the CustomBatteries API to patch this item as a Battery
            {
                EnergyCapacity = 5250,
                ID = "VolatileBattery",
                Name = "Volatile Radioactive Battery",
                FlavorText = "This Battery pulsates with Radioactive energy. Extreme caution when handling; Contains too much Uranium!",
                CraftingMaterials = { nBattery.TechType, TechType.UraniniteCrystal, TechType.UraniniteCrystal, TechType.UraniniteCrystal, TechType.Lead, TechType.Lead, TechType.Sulphur, TechType.Sulphur, TechType.Sulphur },
                UnlocksWith = nBattery.TechType,
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "VolatileBattery.png")),
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "VolatileBatteryskin.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "VolatileBatteryillum.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "VolatileBatteryspec.png")),
                    UseIonModelsAsBase = true,
                    CustomIllumStrength = 1.2f
                },

            };
            unBattery.Patch();

            var unPowercell = new CbPowerCell // Calling the CustomBatteries API to patch this item as a Power Cell
            {
                EnergyCapacity = unBattery.EnergyCapacity * 2,
                ID = "VolatilePowercell",
                Name = "Volatile Radioactive Power Cell",
                FlavorText = "This Power Cell pulsates with Radioactive energy. Extreme caution when handling; Contains too much Uranium!",
                CraftingMaterials = { unBattery.TechType, unBattery.TechType, TechType.Silicone },
                UnlocksWith = unBattery.TechType,
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "VolatileCell.png")),
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "VolatileCellskin.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "VolatileCellillum.png")),
                    UseIonModelsAsBase = true,
                    CustomIllumStrength = 1.2f
                },
            };
            unPowercell.Patch();
        }
        #endregion
    }
}
