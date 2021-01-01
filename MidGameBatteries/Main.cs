using QModManager.API.ModLoading;
using SMLHelper.V2.Utility;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MidGameBatteries.Configuration;
using CustomBatteries.API;
namespace MidGameBatteries
{
    [QModCore]
    public static class Main
    {
        internal static Config Config { get; } = new Config();
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");

        public const string version = "1.0.0.0";
        [QModPatch]
        public static void Load()
        {
            Config.Load();
            CreateAndPatchPack();
        }
        private static void CreateAndPatchPack()
        {
            var deepBattery = new CbBattery()
            {
                EnergyCapacity = Config.DeepBatteryEnergy,
                ID = "CBDeepBattery",
                Name = "Deep Battery",
                FlavorText = "A longer lasting batterry created from rare materials and stronger chemicals.",
                CraftingMaterials = new List<TechType>()
                {
                    TechType.WhiteMushroom, TechType.WhiteMushroom,
                    TechType.Lithium,
                    TechType.AluminumOxide,
                    TechType.Magnetite
                },
                UnlocksWith = TechType.WhiteMushroom,
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "DeepBattery.png")),
                CBModelData = new CBModelData()
                {
                    UseIonModelsAsBase = false,
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "DeepBatteryskin.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "DeepBatteryskin.png")),
                },
            };
            deepBattery.Patch();

            var deepCell = new CbPowerCell()
            {
                EnergyCapacity = Config.DeepCellEnergy,
                ID = "CBDeepCell",
                Name = "Deep Power Cell",
                FlavorText = "A long lasting power cell created from rare materials and stronger chemicals.",
                UnlocksWith = deepBattery.TechType,
                CraftingMaterials = new List<TechType>()
                {
                    deepBattery.TechType, deepBattery.TechType,
                    TechType.Silicone
                },
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "DeepCell.png")),
                CBModelData = new CBModelData()
                {
                    UseIonModelsAsBase = false,
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "DeepCellskin.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "DeepCellskin.png")),
                },
            };
            deepCell.Patch();
        }
    }
}
