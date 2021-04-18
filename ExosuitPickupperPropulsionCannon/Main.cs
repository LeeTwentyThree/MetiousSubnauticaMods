using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;

namespace ExosuitPickupperPropulsionCannon
{
    [QModCore]
    public static class Main
    {
        static Assembly _assembly = Assembly.GetExecutingAssembly();
        
        public const string version = "1.0.0.0";
        
        [QModPatch]
        public static void Load()
        {
            Harmony.CreateAndPatchAll(_assembly, $"Metious_{_assembly.GetName().Name}");
        }
    }
}