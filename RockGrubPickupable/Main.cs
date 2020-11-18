using QModManager.API.ModLoading;
using System.Reflection;
using System.IO;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using Sprite = Atlas.Sprite;
using HarmonyLib;
//using SMLHelper.V2.Commands;
namespace RockGrubPickupable
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        [QModPatch]
        public static void Load()
        {
            InventoryAndWaterParkSetup();
            Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
            //ConsoleCommandsHandler.Main.RegisterConsoleCommand("metiousit", typeof(Main), nameof(MetiousIt));
        }
        #region Inventory and WaterPark Setup
        private static void InventoryAndWaterParkSetup()
        {
            LanguageHandler.SetTechTypeName(TechType.Rockgrub, "RockGrub");
            LanguageHandler.SetTechTypeTooltip(TechType.Rockgrub, "A small, luminescent scavenger");


            Sprite rockgrub = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "RockGrub.png"));
            if (rockgrub != null)
            {
                SpriteHandler.RegisterSprite(TechType.Rockgrub, rockgrub);
            }
            CraftDataHandler.SetItemSize(TechType.Rockgrub, new Vector2int(1, 1));
            WaterParkCreature.waterParkCreatureParameters[TechType.Rockgrub] = new WaterParkCreatureParameters(0.03f, 0.7f, 1f, 1f, true);
            CraftData.IsAllowed(TechType.Rockgrub);
            BioReactorHandler.SetBioReactorCharge(TechType.Rockgrub, 350f);
        }
        /*[ConsoleCommand("metiousit")]
        public static void MetiousIt(string name)
        {
            if (name == "rockgrub")
                CraftData.AddToInventory(TechType.Rockgrub, 1, false, true);
        }*/
        #endregion
    }
}
