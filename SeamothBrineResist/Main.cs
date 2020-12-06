using HarmonyLib;
using QModManager.API.ModLoading;
using System.Reflection;
using SeamothBrineResist.Modules;
namespace SeamothBrineResist
{
    [QModCore]
    public static class Main
    {
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
