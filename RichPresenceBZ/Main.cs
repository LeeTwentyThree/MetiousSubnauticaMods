using System.IO;
using System.Reflection;
using QModManager.API.ModLoading;
using RichPresenceBZ.MonoBehaviours;

namespace RichPresenceBZ;

[QModCore]
public static class Main
{
    public static string AssetsFolder { get; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");

    public const string kVersion = "1.0.0.0";
        
    [QModPatch]
    public static void Load() => DiscordRPManager.Initialize();
}