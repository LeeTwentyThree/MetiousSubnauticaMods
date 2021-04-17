using HarmonyLib;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using QModManager.API.ModLoading;
using System.Reflection;
using System.IO;
using CameraDronePickup.Modules;

namespace CameraDronePickup
{
    [QModCore]
    public static class Main
    {
        internal static DronePickupModule pickupModule;
        
        static Assembly myAssembly = Assembly.GetExecutingAssembly();
        static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        
        public const string version = "1.0.0.0";

        [QModPatch]
        public static void Load()
        {
            pickupModule = new();
            pickupModule.Patch();
            
            Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
        }
    }
}