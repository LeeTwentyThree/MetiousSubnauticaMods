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

namespace AbyssBatteries
{
    [QModCore]
    public static class Main
    {
        internal static Config Config { get; } = OptionsPanelHandler.Main.RegisterModOptions<Config>();
        internal static Values Values { get; } = new Values();
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        public const string version = "0.0.0.7";
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
                Values.Load();
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
            var ghostPiece = new GhostPiece();
            ghostPiece.Patch();

            var ghostDna = new GhostDNA();
            ghostDna.Patch();
            #endregion
            #region Batteries
            JellyBattery = new CbBattery()
            {
                EnergyCapacity = Values.JellyBatteryEnergy,
                ID = "JellyBattery",
                Name = "Jelly Battery",
                FlavorText = "Jelly Battery Comes out of JellyShrooms",
                CraftingMaterials = { TechType.JellyPlantSeed, TechType.Copper, TechType.Titanium },
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "JellyBattery.png")),
                UnlocksWith = TechType.None,
                AddToFabricator = false,
            };
            abyssBatteries.Add(JellyBattery.TechType);
            PoopBattery = new CbBattery()
            {
                EnergyCapacity = Values.PoopBatteryEnergy,
                ID = "PoopBattery",
                Name = "Poop Battery",
                FlavorText = "a Battery from Poooooooooooop",
                CraftingMaterials = { Config.Complement ? JellyBattery.TechType : TechType.None, TechType.SeaTreaderPoop, TechType.Titanium, TechType.Copper },
                UnlocksWith = TechType.None,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "PoopBattery.png")),
                AddToFabricator = false,
            };
            abyssBatteries.Add(PoopBattery.TechType);
            AmpSquidBattery = new CbBattery()
            {
                EnergyCapacity = Values.AmpSquidBatteryEnergy,
                ID = "AmpSquidBattery",
                Name = "AmpSquid Battery",
                FlavorText = "A Battery comes out of Ampeel and Crabsquid",
                CraftingMaterials = { Config.Complement ? PoopBattery.TechType : TechType.None, TechType.Shocker, TechType.CrabSquid, TechType.Titanium },
                UnlocksWith = TechType.None,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "AmpSquidBattery.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    UseIonModelsAsBase = true
                }
            };
            abyssBatteries.Add(AmpSquidBattery.TechType);
            FossilBattery = new CbBattery()
            {
                EnergyCapacity = Values.FossilBatteryEnergy,
                ID = "FossilBattery",
                Name = "Fossil Battery",
                FlavorText = "a Battery comes out of Fossil",
                CraftingMaterials = { Config.Complement ? AmpSquidBattery.TechType : TechType.None, TechType.Spinefish, TechType.AluminumOxide, TechType.Copper, TechType.Titanium },
                UnlocksWith = TechType.None,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "FossilBattery.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    UseIonModelsAsBase = true
                }
            };
            abyssBatteries.Add(FossilBattery.TechType);
            GhostBattery = new CbBattery()
            {
                EnergyCapacity = Values.GhostBatteryEnergy,
                ID = "GhostBattery",
                Name = "Ghost Battery",
                FlavorText = "A Battery made by the Precursor Technology and its interaction with Ghostish DNA",
                CraftingMaterials = { Config.Complement ? FossilBattery.TechType : TechType.None, TechType.Lithium, ghostDna.TechType, FossilBattery.TechType },
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
            ThermalBattery = new CbBattery()
            {
                EnergyCapacity = Values.ThermalBatteryEnergy,
                ID = "ThermalBattery",
                Name = "Thermal Battery",
                FlavorText = "a Battery from Thermal",
                CraftingMaterials = { Config.Complement ? GhostBattery.TechType : TechType.None, TechType.LavaBoomerang, TechType.AluminumOxide },
                UnlocksWith = TechType.None,
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
            JellyCell = new CbPowerCell()
            {
                EnergyCapacity = JellyBattery.EnergyCapacity * 2,
                ID = "JellyPowerCell",
                Name = "Jelly Power Cell",
                FlavorText = "Jelly Power Cell Comes out of JellyShrooms",
                CraftingMaterials = { JellyBattery.TechType, JellyBattery.TechType, TechType.Silicone },
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "JellyCell.png")),
                UnlocksWith = JellyBattery.TechType,
                AddToFabricator = false,
            };
            abyssBatteries.Add(JellyCell.TechType);

            PoopCell = new CbPowerCell()
            {
                EnergyCapacity = PoopBattery.EnergyCapacity * 2,
                ID = "PoopPowercell",
                Name = "Poop Power Cell",
                FlavorText = "a Power Cell from Poooooooooooop",
                CraftingMaterials = { PoopBattery.TechType, PoopBattery.TechType, TechType.Silicone },
                UnlocksWith = TechType.None,
                AddToFabricator = false,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "PoopCell.png")),
            };
            abyssBatteries.Add(PoopCell.TechType);

            AmpSquidCell = new CbPowerCell()
            {
                EnergyCapacity = AmpSquidBattery.EnergyCapacity * 2,
                ID = "AmpSquidCell",
                Name = "AmpSquid Power Cell",
                FlavorText = "A Power Cell ",
                CraftingMaterials = { AmpSquidBattery.TechType, AmpSquidBattery.TechType, TechType.Silicone },
                UnlocksWith = TechType.None,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "AmpSquidCell.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    UseIonModelsAsBase = true
                }
            };

            FossilCell = new CbPowerCell()
            {
                EnergyCapacity = FossilBattery.EnergyCapacity * 2,
                ID = "FossilCell",
                Name = "Fossil Power Cell",
                FlavorText = "a Power Cell comes out of Fossil",
                CraftingMaterials = { FossilBattery.TechType, FossilBattery.TechType, TechType.Silicone },
                UnlocksWith = TechType.None,
                //CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "FossilCell.png")),
                AddToFabricator = false,
                CBModelData = new CBModelData
                {
                    UseIonModelsAsBase = true
                }
            };
            abyssBatteries.Add(FossilCell.TechType);

            GhostCell = new CbPowerCell()
            {
                EnergyCapacity = GhostBattery.EnergyCapacity * 2,
                ID = "GhostPowercell",
                Name = "Ghost Power Cell",
                FlavorText = "A Power Cell made by the Precursor Technology and its interaction with Nuclear Power and Ghostish DNA",
                CraftingMaterials = { GhostBattery.TechType, GhostBattery.TechType, TechType.Silicone },
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

            ThermalCell = new CbPowerCell()
            {
                EnergyCapacity = ThermalBattery.EnergyCapacity * 2,
                ID = "ThermalPowerCell",
                Name = "Thermal Power Cell",
                FlavorText = "a Power Cell from Thermal",
                CraftingMaterials = { ThermalBattery.TechType, ThermalBattery.TechType, TechType.Silicone },
                UnlocksWith = TechType.None,
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
    }
}
