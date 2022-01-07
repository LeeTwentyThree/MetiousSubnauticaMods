using System.IO;
using System.Reflection;
using QModManager.API.ModLoading;
using RichPresenceBZ.MonoBehaviours;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RichPresenceBZ
{
    [QModCore]
    public static class Main
    {
        public static string AssetsFolder { get; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets");
        
        [QModPatch]
        public static void Load() => DiscordRPManager.Initialize();
    }
}