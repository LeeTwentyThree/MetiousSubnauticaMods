using QModManager.API.ModLoading;
using Logger = QModManager.Utility.Logger;
using SMLHelper.V2.Utility;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using CustomBatteries.API;
using BioChemicalBatteries2.Prefabs;
using BioChemicalBatteries2.Configuration;
namespace BioChemicalBatteries2
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static Config Config { get; } = new Config();
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        internal static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, "biochemicalbatteries2"));
        public const string version = "1.0.0.0";
        [QModPatch]
        public static void Load()
        {
            Logger.Log(Logger.Level.Info, "Started Patching");

            Config.Load();
            CreateAndPatchPacks();

            Logger.Log(Logger.Level.Info, "Patching Complete");
        }
        private static void CreateAndPatchPacks()
        {
            Logger.Log(Logger.Level.Info, "Started Patching BioPlasma MK2");

            var bioPlasma = new BioPlasma();
            bioPlasma.Patch();

            Logger.Log(Logger.Level.Info, "Finished Patching BioPlasma MK2");

            var bioChemBattery = new CbBattery()
            {
                ID = "BioChemBatteryMK2",
                Name = "Biochemical Battery",
                FlavorText = "Alterra battery technology combined with a Warper power core makes for quite a potent renewable energy source.",
                EnergyCapacity = Config.BioChemBatteryEnergy,
                UnlocksWith = bioPlasma.TechType,
                CraftingMaterials = new List<TechType>()
                {
                    bioPlasma.TechType, bioPlasma.TechType,
                    TechType.Silver, TechType.Silver,
                    TechType.Gold,
                    TechType.Lead
                },
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "BioChemBattery.png")),
                CBModelData = new CBModelData()
                {
                    UseIonModelsAsBase = true,
                    CustomTexture = assetBundle.LoadAsset<Texture2D>("BioChemBatteryskin"),
                    CustomSpecMap = assetBundle.LoadAsset<Texture2D>("BioChemBatteryspec"),
                    CustomIllumMap = assetBundle.LoadAsset<Texture2D>("BioChemBatteryillum"),
                },
            };
            bioChemBattery.Patch();

            var bioChemCell = new CbPowerCell()
            {
                ID = "BioChemCellMK2",
                Name = "Biochemical Power Cell",
                FlavorText = "Alterra power cell technology combined with a Warper power core makes for quite a potent renewable energy source.",
                EnergyCapacity = Config.BioChemCellenergy,
                UnlocksWith = bioPlasma.TechType,
                CraftingMaterials = new List<TechType>()
                {
                    bioChemBattery.TechType, bioChemBattery.TechType,
                    TechType.Silicone
                },
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "BioChemCell.png")),
                CBModelData = new CBModelData()
                {
                    UseIonModelsAsBase = true,
                    CustomTexture = assetBundle.LoadAsset<Texture2D>("BioChemCellskin"),
                    CustomSpecMap = assetBundle.LoadAsset<Texture2D>("BioChemCellskin"),
                    CustomIllumMap = assetBundle.LoadAsset<Texture2D>("BioChemCellillum")
                },
            };
            bioChemCell.Patch();
        }
    }
}
