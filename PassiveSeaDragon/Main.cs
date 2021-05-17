using HarmonyLib;
using QModManager.API.ModLoading;
using System.Reflection;

namespace PassiveSeaDragon
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        public const string version = "1.0.0.0";
        [QModPatch]
        public static void Load() => Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
    }
}
