using HarmonyLib;
using QModManager.API.ModLoading;
using System.Reflection;

namespace SeamothBrineResist
{
    [QModCore]
    public static class Main
    {
        [QModPatch]
        public static void Load()
        {
            var assembly = Assembly.GetExecutingAssembly();
            new Harmony($"Metious_{assembly.GetName().Name}").PatchAll(assembly);
        }
    }
}
