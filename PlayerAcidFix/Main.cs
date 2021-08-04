using HarmonyLib;
using System.Reflection;
using QModManager.API.ModLoading;

namespace PlayerAcidFix
{
    [QModCore]
    public static class Main
    {
        static Assembly myAssembly = Assembly.GetExecutingAssembly();
        
        [QModPatch]
        public static void Load() => Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
    }
}
