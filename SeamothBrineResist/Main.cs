using HarmonyLib;
using QModManager.API.ModLoading;
using System.Reflection;
using System.IO;
using SeamothBrineResist.Modules;
namespace SeamothBrineResist
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
            var brineResist = new SeamothBrineResistanceModule();
            brineResist.Patch();
            var assembly = Assembly.GetExecutingAssembly();
            new Harmony($"Metious_{assembly.GetName().Name}").PatchAll(assembly);
        }
    }
}
