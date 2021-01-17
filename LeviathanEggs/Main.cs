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

            var sdEggModel = Resources.Load<GameObject>("WorldEntities/Eggs/EmperorEgg");
            var seaDragonEgg = new SeaEmperorEgg(sdEggModel);
            seaDragonEgg.Patch();

            //Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
        }
        private static void WaterParkCreatureParametersSettings()
        {
            LanguageHandler.SetTechTypeName(TechType.SeaEmperorBaby, "Sea Emperor Baby");
            LanguageHandler.SetTechTypeName(TechType.SeaEmperorJuvenile, "Sea Emperor Juvenile");
            LanguageHandler.SetTechTypeName(TechType.SeaEmperor, "Sea Emperor");

            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperorBaby, "Sea Emperor Baby");
            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperorJuvenile, "Sea Emperor Juvenile");
            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperor, "Sea Emperor");

            SpriteHandler.RegisterSprite(TechType.SeaEmperorBaby, SpriteManager.Get(TechType.Titanium));
            SpriteHandler.RegisterSprite(TechType.SeaEmperorJuvenile, SpriteManager.Get(TechType.Titanium));
            SpriteHandler.RegisterSprite(TechType.SeaEmperor, SpriteManager.Get(TechType.Titanium));

            CraftDataHandler.SetItemSize(TechType.SeaEmperorBaby, new Vector2int(1, 1));
            CraftDataHandler.SetItemSize(TechType.SeaEmperorJuvenile, new Vector2int(1, 1));
            CraftDataHandler.SetItemSize(TechType.SeaEmperor, new Vector2int(1, 1));

            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperor] = new WaterParkCreatureParameters(0.03f, 0.07f, 1f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperorJuvenile] = new WaterParkCreatureParameters(0.03f, 0.07f, 1f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperorBaby] = new WaterParkCreatureParameters(0.03f, 0.07f, 1f, 1f, false);
        }
    }
}
