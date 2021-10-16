using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;

namespace AdditionalCyclopsLightController
{
    [QModCore]
    public static class Main
    {
        private static Assembly _assembly = Assembly.GetExecutingAssembly();

        [QModPatch]
        public static void Load() => Harmony.CreateAndPatchAll(_assembly, $"Metious_{_assembly.GetName().Name}");
    }
}