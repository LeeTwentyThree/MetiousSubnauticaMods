using HarmonyLib;
using QModManager.API.ModLoading;
using System.IO;
using System.Reflection;
namespace ColorizableSpotlight
{
    [QModCore]
    public static class Main
    {
        static Assembly myAssembly = Assembly.GetExecutingAssembly();
        static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        public static string Combine(string path1, string path2) => Path.Combine(path1, path2).Replace('\\', '/');
        public static string GetSaveFolderPath() => Path.GetFullPath(Path.Combine(ModPath, "../../SNAppData/SavedGames/", SaveLoadManager.main.GetCurrentSlot(), "ColorizedSpotlight")).Replace('\\', '/');
        
        public const string version = "1.0.6.0";
        [QModPatch]
        public static void Load()
        {
            Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
        }
    }
}
