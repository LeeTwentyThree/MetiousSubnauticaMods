using HarmonyLib;
using QModManager.API.ModLoading;
using System.IO;
using System.Reflection;
using ExosuitSolarChargerBZ.Modules;
namespace ExosuitSolarChargerBZ
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        internal static ExosuitSolarChargerModule exosuitSolar = new ExosuitSolarChargerModule();
        public const string version = "1.0.0.0";
        [QModPatch]
        public static void Load()
        {
            exosuitSolar.Patch();
            Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
        }
    }
}
