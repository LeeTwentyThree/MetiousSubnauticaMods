using HarmonyLib;
using QModManager.API.ModLoading;
using System;
using System.Reflection;
using System.IO;
using CustomBatteries.API;
using SMLHelper.V2.Utility;
using AbyssBatteries.Items;
using AbyssBatteries.Craftables;
using UnityEngine;
using AbyssBatteries.MonoBehaviours;
using System.Collections.Generic;
using Common;
using System.Linq;
using AbyssBatteries.Configuration;
using SMLHelper.V2.Handlers;
using Sprite = Atlas.Sprite;
namespace AbyssBatteries
{
    [QModCore]
    public static class Main
    {
        internal static Config Config { get; private set; }
        internal static Values Values { get; } = new Values();
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        public const string version = "0.0.0.11";
        public const string modName = "[AbyssBatteries] ";
        #region Tabs
        public const string AbyssBatteryTab = "aBatteryTab";
        public const string AbyssPowCellTab = "aPowCell";
        public const string AbyssResourcesTab = "AbyssResources";
        #endregion

        #region static Batteries
        public static List<TechType> abyssBatteries = new List<TechType>();

        private static CbBattery _jBattery = null;
        private static CbPowerCell _jCell = null;

        private static CbBattery _pBattery = null;
        private static CbPowerCell _pCell = null;

        private static CbBattery _asBattery = null;
        private static CbPowerCell _asCell = null;

        private static CbBattery _fBattery = null;
        private static CbPowerCell _fCell = null;

        private static CbBattery _gBattery = null;
        private static CbPowerCell _gCell = null;

        private static CbBattery _tBattery = null;
        private static CbPowerCell _tCell = null;

        public static CbBattery JellyBattery
        {
            get
            {
                return _jBattery;
            }
            private set
            {
                _jBattery = value;
                _jBattery.Patch();
            }
        }
        public static CbPowerCell JellyCell
        {
            get
            {
                return _jCell;
            }
            private set
            {
                _jCell = value;
                _jCell.Patch();
            }
        }

        public static CbBattery PoopBattery
        {
            get
            {
                return _pBattery;
            }
            private set
            {
                _pBattery = value;
                _pBattery.Patch();
            }
        }
        public static CbPowerCell PoopCell
        {
            get
            {
                return _pCell;
            }
            private set
            {
                _pCell = value;
                _pCell.Patch();
            }
        }
        public static CbBattery AmpSquidBattery
        {
            get
            {
                return _asBattery;
            }
            private set
            {
                _asBattery = value;
                _asBattery.Patch();
            }
        }
        public static CbPowerCell AmpSquidCell
        {
            get
            {
                return _asCell;
            }
            private set
            {
                _asCell = value;
                _asCell.Patch();
            }
        }

        public static CbBattery FossilBattery
        {
            get
            {
                return _fBattery;
            }
            private set
            {
                _fBattery = value;
                _fBattery.Patch();
            }
        }
        public static CbPowerCell FossilCell
        {
            get
            {
                return _fCell;
            }
            private set
            {
                _fCell = value;
                _fCell.Patch();
            }
        }

        public static CbBattery GhostBattery
        {
            get
            {
                return _gBattery;
            }
            private set
            {
                _gBattery = value;
                _gBattery.Patch();
            }
        }
        public static CbPowerCell GhostCell
        {
            get
            {
                return _gCell;
            }
            private set
            {
                _gCell = value;
                _gCell.Patch();
            }
        }

        public static CbBattery ThermalBattery
        {
            get
            {
                return _tBattery;
            }
            private set
            {
                _tBattery = value;
                _tBattery.Patch();
            }
        }
        public static CbPowerCell ThermalCell
        {
            get
            {
                return _tCell;
            }
            private set
            {
                _tCell = value;
                _tCell.Patch();
            }
        }
        #endregion


        [QModPatch]
        public static void Load()
        {
            MetiousLogger.PatchStart(modName, version);
            try
            {
                Config = OptionsPanelHandler.RegisterModOptions<Config>();
                IngameMenuHandler.RegisterOnSaveEvent(Config.Save);
                Values.Load();
                InventorySetup();
                CreateAndPatchPacks();
                var aFabricator = new AbyssFabricator();
                #region TabNodes
                aFabricator.AddTabNode(AbyssBatteryTab, "AbyssBatteries", SpriteManager.Get(TechType.Battery));
                aFabricator.AddTabNode(AbyssPowCellTab, "AbyssPowerCells", SpriteManager.Get(TechType.PowerCell));
                aFabricator.AddTabNode(AbyssResourcesTab, "Resources", SpriteManager.Get(TechType.Aerogel));
                #endregion
                #region CraftNodes
                aFabricator.AddCraftNode(GhostDNA.TechTypeID, AbyssResourcesTab);
                aFabricator.AddCraftNode(JellyBattery.TechType, AbyssBatteryTab);
                aFabricator.AddCraftNode(JellyCell.TechType, AbyssPowCellTab);
                aFabricator.AddCraftNode(PoopBattery.TechType, AbyssBatteryTab);
                aFabricator.AddCraftNode(PoopCell.TechType, AbyssPowCellTab);
                aFabricator.AddCraftNode(AmpSquidBattery.TechType, AbyssBatteryTab);
                aFabricator.AddCraftNode(AmpSquidCell.TechType, AbyssPowCellTab);
                aFabricator.AddCraftNode(FossilBattery.TechType, AbyssBatteryTab);
                aFabricator.AddCraftNode(FossilCell.TechType, AbyssPowCellTab);
                aFabricator.AddCraftNode(GhostBattery.TechType, AbyssBatteryTab);
                aFabricator.AddCraftNode(GhostCell.TechType, AbyssPowCellTab);
                aFabricator.AddCraftNode(ThermalBattery.TechType, AbyssBatteryTab);
                aFabricator.AddCraftNode(ThermalCell.TechType, AbyssPowCellTab);
                #endregion
                aFabricator.Patch();
                Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
                MetiousLogger.PatchComplete(modName);
            }
            catch (Exception e)
            {
                MetiousLogger.PatchFailed(modName, e);
            }
        }
        private static void CreateAndPatchPacks()
        {
            #region Custom Items Patching
            var reefEnzyme = new ReefbackEnzyme();
            reefEnzyme.Patch();

            var ghostPiece = new GhostPiece();
            ghostPiece.Patch();

            var ghostDna = new GhostDNA();
            ghostDna.Patch();

            var dragonScale = new DragonScale();
            dragonScale.Patch();
            #endregion
            #region Batteries
            JellyBattery = new CbBattery()
            {
                EnergyCapacity = Values.JellyBatteryEnergy,
                ID = "JellyBattery",
                Name = "Cavernous Jellyshroom Battery",
                FlavorText = "A battery composed of a strange, organic filament found on Crabsnake eggs and the incredibly high-energy content seeds of Jellyshrooms, as well as an assortment of minerals found in the Jellyshroom Caves.",
                CraftingMaterials = { Config.Complement ? TechType.Battery : TechType.Titanium, Config.Complement ? TechType.None : TechType.Quartz, TechType.SnakeMushroomSpore, TechType.CrabsnakeEgg, TechType.Gold, TechType.Magnetite, TechType.Magnetite },
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "JellyBattery.png")),
                UnlocksWith = TechType.SnakeMushroomSpore,
                AddToFabricator = false,
            };
            abyssBatteries.Add(JellyBattery.TechType);
            List<TechType> complementPoopBattery = new List<TechType> { JellyBattery.TechType, reefEnzyme.TechType, TechType.SeaTreaderPoop, TechType.Gold };
            List<TechType> nonComplementPoopBattery = new List<TechType> { reefEnzyme.TechType, TechType.SeaTreaderPoop, TechType.Lithium, TechType.Lithium, TechType.AluminumOxide, TechType.Quartz, TechType.Titanium, TechType.Diamond };
            PoopBattery = new CbBattery()
            {
                EnergyCapacity = Values.PoopBatteryEnergy,
                ID = "PoopBattery",
                Name = "Reefback Enzyme-Charged Poop Battery",
                FlavorText = "A battery fuelled by a potent chemical reaction between a Reefback's pod enzymes and a specific combination of minerals and organic compounds found in a Sea Treader's alien feces",
                CraftingMaterials = Config.Complement ? complementPoopBattery : nonComplementPoopBattery,
                UnlocksWith = TechType.SeaTreaderPoop,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "PoopBattery.png")),
                AddToFabricator = false,
            };
            abyssBatteries.Add(PoopBattery.TechType);

            List<TechType> complementAmpSquidBattery = new List<TechType> { PoopBattery.TechType, TechType.Shocker, TechType.CrabSquid, TechType.CopperWire, TechType.Silicone };
            List<TechType> nonComplementAmpSquidattery = new List<TechType> { TechType.Shocker, TechType.CrabSquid, TechType.CopperWire, TechType.WiringKit, TechType.Gold, TechType.Silicone, TechType.BloodOil, TechType.Quartz, TechType.Titanium };
            AmpSquidBattery = new CbBattery()
            {
                EnergyCapacity = Values.AmpSquidBatteryEnergy,
                ID = "AmpSquidBattery",
                Name = "High Voltage Electro-Magnetic Battery",
                FlavorText = "This Battery combines the Ampeel's tremendous bioelectric capacity with the complex EMP organ of the CrabSquid, harnessing the resulting energy and sorting it into a single, portable, rrechargeable source",
                CraftingMaterials = Config.Complement ? complementAmpSquidBattery : nonComplementAmpSquidattery,
                UnlocksWith = Config.Complement ? TechType.CopperWire : TechType.Gold,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "AmpSquidBattery.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    UseIonModelsAsBase = true
                }
            };
            abyssBatteries.Add(AmpSquidBattery.TechType);

            List<TechType> complementFossilBattery = new List<TechType> { AmpSquidBattery.TechType, TechType.SpineEel, TechType.MesmerEgg, TechType.RedGreenTentacleSeed, TechType.Sulphur, TechType.Sulphur, TechType.Nickel };
            List<TechType> nonComplementFossilBattery = new List<TechType> { TechType.SpineEel, TechType.MesmerEgg, TechType.RedGreenTentacleSeed, TechType.Sulphur, TechType.Sulphur, TechType.Sulphur, TechType.Nickel, TechType.Nickel, TechType.UraniniteCrystal, TechType.Titanium, TechType.Quartz };
            FossilBattery = new CbBattery()
            {
                EnergyCapacity = Values.FossilBatteryEnergy,
                ID = "FossilBattery",
                Name = "Fossil-Fueled Battery",
                FlavorText = "A battery made out of an assortment of creatures and minerals found in the Lost River",
                CraftingMaterials = Config.Complement ? complementFossilBattery : nonComplementFossilBattery,
                UnlocksWith = TechType.MesmerEgg,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "FossilBattery.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    UseIonModelsAsBase = true
                }
            };
            abyssBatteries.Add(FossilBattery.TechType);

            List<TechType> complementGhostBattery = new List<TechType> { FossilBattery.TechType, TechType.Lithium, GhostDNA.TechTypeID, TechType.AdvancedWiringKit, TechType.PrecursorIonCrystal };
            List<TechType> nonComplementGhostBattery = new List<TechType> { TechType.Lithium, GhostDNA.TechTypeID, TechType.AdvancedWiringKit, TechType.AluminumOxide, TechType.AluminumOxide, TechType.AluminumOxide, TechType.Titanium, TechType.Quartz, TechType.PrecursorIonCrystal, TechType.PrecursorIonCrystal, TechType.Diamond };
            GhostBattery = new CbBattery()
            {
                EnergyCapacity = Values.GhostBatteryEnergy,
                ID = "GhostBattery",
                Name = "Ghost-Charged Plasma Battery",
                FlavorText = "A mysterious golden substance runs deep through the Ghost Leviathan's veins, which, when combined with its natural bioelectricity produces a plasma of unprecedented power.",
                CraftingMaterials = Config.Complement ? complementGhostBattery : nonComplementGhostBattery,
                UnlocksWith = ghostPiece.TechType,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "GhostBattery.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "GhostBatteryskin.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "GhostBatteryillum.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "GhostBatteryspec.png")),
                    UseIonModelsAsBase = true,
                    CustomIllumStrength = 1.5f
                },
                EnhanceGameObject = new Action<GameObject>((go) => EnhanceGameObject(go))
            };
            abyssBatteries.Add(GhostBattery.TechType);
            List<TechType> complementThermalBattery = new List<TechType> { GhostBattery.TechType, TechType.Kyanite, TechType.Kyanite, dragonScale.TechType, TechType.PrecursorIonCrystal };
            List<TechType> nonComplementThermalBattery = new List<TechType> { dragonScale.TechType, TechType.Kyanite, TechType.Kyanite, TechType.PrecursorIonCrystal, TechType.PrecursorIonCrystal, TechType.PrecursorIonCrystal, TechType.Aerogel, TechType.Aerogel, TechType.Quartz, TechType.Titanium, TechType.Diamond, TechType.Diamond };
            ThermalBattery = new CbBattery()
            {
                EnergyCapacity = Values.ThermalBatteryEnergy,
                ID = "ThermalBattery",
                Name = "Dragon-Energized Thermal Battery",
                FlavorText = "This Battery combines state-of-the-art Alterran thermal-charging technology with the superlative heat-resistant qualities of Sea Dragon scales.",
                CraftingMaterials = Config.Complement ? complementThermalBattery : nonComplementThermalBattery,
                UnlocksWith = dragonScale.TechType,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "ThermalBattery.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    UseIonModelsAsBase = true
                }
            };
            abyssBatteries.Add(ThermalBattery.TechType);
            #endregion
            #region PowerCells
            List<TechType> complementJellyCell = new List<TechType> { TechType.PowerCell, JellyBattery.TechType, TechType.Titanium };
            List<TechType> nonComplementJellyCell = new List<TechType> { JellyBattery.TechType, JellyBattery.TechType, TechType.Silicone };
            JellyCell = new CbPowerCell()
            {
                EnergyCapacity = JellyBattery.EnergyCapacity * 2,
                ID = "JellyPowerCell",
                Name = "Cavernous Jellyshroom Power Cell",
                FlavorText = "A Power Cell composed of a strange, organic filament found on Crabsnake eggs and the incredibly high-energy content seeds of Jellyshrooms, as well as an assortment of minerals found in the Jellyshroom Caves.",
                CraftingMaterials = Config.Complement ? complementJellyCell : nonComplementJellyCell,
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "JellyCell.png")),
                UnlocksWith = JellyBattery.TechType,
                AddToFabricator = false,
            };
            abyssBatteries.Add(JellyCell.TechType);

            List<TechType> complementPoopCell = new List<TechType> { JellyCell.TechType, PoopBattery.TechType, TechType.Titanium };
            List<TechType> nonComplementPoopCell = new List<TechType> { PoopBattery.TechType, PoopBattery.TechType, TechType.Silicone };
            PoopCell = new CbPowerCell()
            {
                EnergyCapacity = PoopBattery.EnergyCapacity * 2,
                ID = "PoopPowercell",
                Name = "Poop Power Cell",
                FlavorText = "A Power Cell fuelled by a potent chemical reaction between a Reefback's pod enzymes and a specific combination of minerals and organic compounds found in a Sea Treader's alien feces",
                CraftingMaterials = Config.Complement ? complementPoopCell : nonComplementPoopCell,
                UnlocksWith = PoopBattery.TechType,
                AddToFabricator = false,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "PoopCell.png")),
            };
            abyssBatteries.Add(PoopCell.TechType);

            List<TechType> complementAmpSquidCell = new List<TechType> { PoopCell.TechType, AmpSquidBattery.TechType, TechType.Lead };
            List<TechType> nonComplementAmpSquidCell = new List<TechType> { AmpSquidBattery.TechType, AmpSquidBattery.TechType, TechType.Silicone };
            AmpSquidCell = new CbPowerCell()
            {
                EnergyCapacity = AmpSquidBattery.EnergyCapacity * 2,
                ID = "AmpSquidCell",
                Name = "High Voltage Electro-Magnetic Power Cell",
                FlavorText = "This Power Cell combines the Ampeel's tremendous bioelectric capacity with the complex EMP organ of the CrabSquid, harnessing the resulting energy and sorting it into a single, rechargeable, portable source",
                CraftingMaterials = Config.Complement ? complementAmpSquidCell : nonComplementAmpSquidCell,
                UnlocksWith = AmpSquidBattery.TechType,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "AmpSquidCell.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    UseIonModelsAsBase = true
                }
            };
            abyssBatteries.Add(AmpSquidCell.TechType);
            List<TechType> complementFossilCell = new List<TechType> { AmpSquidCell.TechType, FossilBattery.TechType, TechType.Lead };
            List<TechType> nonComplementFossilCell = new List<TechType> { FossilBattery.TechType, FossilBattery.TechType, TechType.Silicone };
            FossilCell = new CbPowerCell()
            {
                EnergyCapacity = FossilBattery.EnergyCapacity * 2,
                ID = "FossilCell",
                Name = "Fossil-Fueled Power Cell",
                FlavorText = "A Power Cell made out of an assortment of creatures and minerals found in the Lost River",
                CraftingMaterials = Config.Complement ? complementFossilCell : nonComplementFossilCell,
                UnlocksWith = FossilBattery.TechType,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "FossilCell.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    UseIonModelsAsBase = true
                }
            };
            abyssBatteries.Add(FossilCell.TechType);

            List<TechType> complementGhostCell = new List<TechType> { FossilCell.TechType, GhostBattery.TechType, TechType.Lithium };
            List<TechType> nonComplementGhostCell = new List<TechType> { GhostBattery.TechType, GhostBattery.TechType, TechType.Silicone };
            GhostCell = new CbPowerCell()
            {
                EnergyCapacity = GhostBattery.EnergyCapacity * 2,
                ID = "GhostPowercell",
                Name = "Ghost-Charged Plasma Power Cell",
                FlavorText = "A mysterious golden substance runs deep through the Ghost Leviathan's veins, which, when combined with its natural bioelectricity produces a plasma of unprecedented power.",
                CraftingMaterials = Config.Complement ? complementGhostCell : nonComplementGhostCell,
                UnlocksWith = GhostBattery.TechType,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "GhostCell.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "GhostCellskin.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "GhostCellillum.png")),
                    UseIonModelsAsBase = true,
                    CustomIllumStrength = 1.5f
                },
                EnhanceGameObject = new Action<GameObject>((go) => EnhanceGameObject(go)),
            };
            abyssBatteries.Add(GhostCell.TechType);

            List<TechType> complementThermalCell = new List<TechType> { GhostCell.TechType, ThermalBattery.TechType, TechType.Lithium };
            List<TechType> nonComplementThermalCell = new List<TechType> { ThermalBattery.TechType, ThermalBattery.TechType, TechType.Silicone };
            ThermalCell = new CbPowerCell()
            {
                EnergyCapacity = ThermalBattery.EnergyCapacity * 2,
                ID = "ThermalPowerCell",
                Name = "Dragon-Energized Thermal Power Cell",
                FlavorText = "This Power Cell combines state-of-the-art Alterran thermal-charging technology with the superlative heat-resistant qualities of Sea Dragon scales.",
                CraftingMaterials = Config.Complement ? complementThermalCell : nonComplementThermalCell,
                UnlocksWith = ThermalBattery.TechType,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "ThermalCell.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    UseIonModelsAsBase = true
                },

            };
            abyssBatteries.Add(ThermalCell.TechType);
            #endregion
        }
        public static void EnhanceGameObject(GameObject gameObject)
        {
            gameObject.EnsureComponent<PulsatingBehaviour>();
        }
        private static void InventorySetup()
        {
            LanguageHandler.SetTechTypeName(TechType.SpineEel, "River Prowler");
            LanguageHandler.SetTechTypeTooltip(TechType.SpineEel, "A fast, agile predator discovered at great depths");

            Sprite spineEel = ImageUtils.LoadSpriteFromFile(Path.Combine(Main.AssetsFolder, "SpineEel.png"));
            if (spineEel != null)
                SpriteHandler.RegisterSprite(TechType.SpineEel, spineEel);
            CraftDataHandler.SetItemSize(TechType.SpineEel, new Vector2int(3, 3));
        }
    }
}
