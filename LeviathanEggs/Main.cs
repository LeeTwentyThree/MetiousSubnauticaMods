using System.Globalization;
using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using System.IO;
using System.Reflection;
using LeviathanEggs.Prefabs;
using UnityEngine;
namespace LeviathanEggs
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        public const string version = "1.0.0.0";
        [QModPatch]
        public static void Load()
        {
            WaterParkCreatureParametersSettings();

            var seEggModel = Resources.Load<GameObject>("WorldEntities/Eggs/EmperorEgg");
            //var seEgg = GameObject.Instantiate(seEggModel);
            var seaEmperorEgg = new SeaEmperorEgg(seEggModel);
            seaEmperorEgg.Patch();

            var sdEggModel = Resources.Load<GameObject>("WorldEntities/Environment/Precursor/LostRiverBase/Precursor_LostRiverBase_SeaDragonEggShell");
            //var sdEgg = GameObject.Instantiate(sdEggModel);
            var seaDragonEgg = new SeaDragonEgg(sdEggModel);
            seaDragonEgg.Patch();

            //var glEggModel = Resources.Load<GameObject>("WorldEntities/Doodads/Lost_river/lost_river_cove_tree_01");
           // GameObject glEgg = GameObject.Instantiate(glEggModel);
            var ghostEgg = new GhostEgg();
            ghostEgg.Patch();

            //seEggModel.SetActive(false);
            //sdEggModel.SetActive(false);
            //glEggModel.SetActive(false);
            //Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
        }
        private static void WaterParkCreatureParametersSettings()
        {
            LanguageHandler.SetTechTypeName(TechType.SeaEmperorBaby, "Sea Emperor Baby");
            LanguageHandler.SetTechTypeName(TechType.SeaEmperorJuvenile, "Sea Emperor Juvenile");
            LanguageHandler.SetTechTypeName(TechType.SeaEmperor, "Sea Emperor");

            LanguageHandler.SetTechTypeName(TechType.SeaDragon, "Sea Dragon");

            LanguageHandler.SetTechTypeName(TechType.GhostLeviathanJuvenile, "Ghost Leviathan Juvenile");

            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperorBaby, "Sea Emperor Baby");
            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperorJuvenile, "Sea Emperor Juvenile");
            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperor, "Sea Emperor");

            LanguageHandler.SetTechTypeTooltip(TechType.SeaDragon, "Sea Dragon");

            LanguageHandler.SetTechTypeTooltip(TechType.GhostLeviathanJuvenile, "Ghost Leviathan Juvenile");

            SpriteHandler.RegisterSprite(TechType.SeaEmperorBaby, SpriteManager.Get(TechType.Titanium));
            SpriteHandler.RegisterSprite(TechType.SeaEmperorJuvenile, SpriteManager.Get(TechType.Titanium));
            SpriteHandler.RegisterSprite(TechType.SeaEmperor, SpriteManager.Get(TechType.Titanium));

            SpriteHandler.RegisterSprite(TechType.SeaDragon, SpriteManager.Get(TechType.Titanium));

            SpriteHandler.RegisterSprite(TechType.GhostLeviathanJuvenile, SpriteManager.Get(TechType.Titanium));

            CraftDataHandler.SetItemSize(TechType.SeaEmperorBaby, new Vector2int(1, 1));
            CraftDataHandler.SetItemSize(TechType.SeaEmperorJuvenile, new Vector2int(1, 1));
            CraftDataHandler.SetItemSize(TechType.SeaEmperor, new Vector2int(1, 1));

            CraftDataHandler.SetItemSize(TechType.SeaDragon, new Vector2int(1, 1));

            CraftDataHandler.SetItemSize(TechType.GhostLeviathanJuvenile, new Vector2int(1, 1));

            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperor] = new WaterParkCreatureParameters(0.03f, 0.05f, 0.07f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperorJuvenile] = new WaterParkCreatureParameters(0.03f, 0.05f, 1f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperorBaby] = new WaterParkCreatureParameters(0.03f, 0.05f, 0.07f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.SeaDragon] = new WaterParkCreatureParameters(0.03f, 0.05f, 0.07f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.GhostLeviathanJuvenile] = new WaterParkCreatureParameters(0.03f, 0.05f, 0.07f, 1f, false);
        }
    }
}
