using System;
using System.IO;
using CustomBatteries.API;
using SMLHelper.V2.Utility;
using System.Reflection;
using QModManager.API.ModLoading;

namespace EnzymeChargedBatteries
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");

        public const string modName = "[EnzymeChargedBatteries] ";
        public const string version = "1.0.2.1";

        [QModPatch]
        public static void Load() => CreateAndPatchPacks();

        #region Create And Patch Packs
        private static void CreateAndPatchPacks()
        {
            var enzymeBattery = new CbBattery // Calling the CustomBatteries API to patch this item as a Battery
            {
                EnergyCapacity = 1000,
                ID = "EnzymeBattery",
                Name = "Enzyme-Charged Ion Battery",
                FlavorText = "A new battery based on the discovery of a chemical interaction between hatching enzymes, radiation, and ion crystals.",
                CraftingMaterials = { TechType.PrecursorIonCrystal, TechType.HatchingEnzymes, TechType.HatchingEnzymes, TechType.Lead, TechType.UraniniteCrystal, TechType.UraniniteCrystal },
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "EnzymeBattery.png")),
                UnlocksWith = TechType.HatchingEnzymes,
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "EnzymeBatteryskin.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "EnzymeBatteryillum.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "EnzymeBatteryspec.png")),
                    CustomIllumStrength = 1.1f,
                    UseIonModelsAsBase = true
                }
            };
            enzymeBattery.Patch();


            var enzymeCell = new CbPowerCell // Calling the CustomBatteries API to patch this item as a Power Cell
            {
                EnergyCapacity = enzymeBattery.EnergyCapacity * 2,
                ID = "EnzymePowerCell",
                Name = "Enzyme-Charged Ion Power Cell",
                FlavorText = "A new power cell based on the discovery of a chemical interaction between hatching enzymes, radiation, and ion crystals.",
                CraftingMaterials = { enzymeBattery.TechType, enzymeBattery.TechType, TechType.Silicone },
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "EnzymeCell.png")),
                UnlocksWith = TechType.HatchingEnzymes,
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "EnzymeCellskin.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "EnzymeCellillum.png")),
                    CustomIllumStrength = 1.1f,
                    UseIonModelsAsBase = true
                }
            };
            enzymeCell.Patch();
        }
        #endregion
    }
}
