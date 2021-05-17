using UnityEngine;
using System.IO;
using CustomBatteries.API;
using System.Reflection;
using QModManager.API.ModLoading;

namespace EnzymeChargedBatteries
{
    [QModCore]
    public static class Main
    {
        static Assembly myAssembly = Assembly.GetExecutingAssembly();
        static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        static string AssetsFolder = Path.Combine(ModPath, "Assets");
        static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, "enzymechargedbatteries"));

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
                CustomIcon = new Atlas.Sprite(assetBundle.LoadAsset<Sprite>("EnzymeBattery.png")),
                UnlocksWith = TechType.HatchingEnzymes,
                CBModelData = new CBModelData
                {
                    CustomTexture = assetBundle.LoadAsset<Texture2D>("EnzymeBatteryskin"),
                    CustomIllumMap = assetBundle.LoadAsset<Texture2D>("EnzymeBatteryillum"),
                    CustomSpecMap = assetBundle.LoadAsset<Texture2D>("EnzymeBatteryspec"),
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
                CustomIcon = new Atlas.Sprite(assetBundle.LoadAsset<Sprite>("EnzymeCell")),
                UnlocksWith = TechType.HatchingEnzymes,
                CBModelData = new CBModelData
                {
                    CustomTexture = assetBundle.LoadAsset<Texture2D>("EnzymeCellskin"),
                    CustomIllumMap = assetBundle.LoadAsset<Texture2D>("EnzymeCellillum"),
                    CustomSpecMap = assetBundle.LoadAsset<Texture2D>("EnzymeCellspec"),
                    CustomIllumStrength = 1.1f,
                    UseIonModelsAsBase = true
                }
            };
            enzymeCell.Patch();
        }
        #endregion
    }
}
