using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using CustomBatteries.API;
using BioChemicalBatteries2.Prefabs;
namespace BioChemicalBatteries2
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        internal static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, "biochemicalbatteries2"));
        public const string version = "1.0.0.0";
        [QModPatch]
        public static void Load()
        {
            CreateAndPatchPacks();
        }
        private static void CreateAndPatchPacks()
        {
            var bioPlasma = new BioPlasma();
            bioPlasma.Patch();

            var bioChemBattery = new CbBattery()
            {
                ID = "BioChemBattery",
                Name = "BioChemical Battery",
                FlavorText = "Alterra Battery technology combined with a Warper power core makes for quite a potent renewable energy source.",
                EnergyCapacity = 2500,
                UnlocksWith = bioPlasma.TechType,
                CraftingMaterials = new List<TechType>()
                {
                    bioPlasma.TechType,
                    TechType.Magnetite, TechType.Magnetite
                },
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "BioChemBattery.png")),
                CBModelData = new CBModelData()
                {
                    UseIonModelsAsBase = true,
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "BioChemBatteryskin.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "BioChemBatteryspec.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "BioChemBatteryillum.png"))
                },
            };
            bioChemBattery.Patch();

            var bioChemCell = new CbBattery()
            {
                ID = "BioChemicalCell",
                Name = "BioChemical Power Cell",
                FlavorText = "Alterra Power Cell technology combined with a Warper power core makes for quite a potent renewable energy source.",
                EnergyCapacity = bioChemBattery.EnergyCapacity * 2,
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
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "BioChemCellskin.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "BioChemCellskin.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "BioChemCellillum.png"))
                },
            };
            bioChemCell.Patch();
        }
    }
}
