using HarmonyLib;
using QModManager.API.ModLoading;
using System.Reflection;

namespace SpadefishHitFix
{
    [QModCore]
    public static class Main
    {
        static readonly Assembly myAssembly = Assembly.GetExecutingAssembly();
        
        public const string Version = "1.0.0.0";

        [QModPatch]
        public static void Load() => Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
    }
}