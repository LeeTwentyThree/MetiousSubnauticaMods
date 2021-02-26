using QModManager.API.ModLoading;
using DrillableSulphur.Prefabs;
namespace DrillableSulphur
{
    [QModCore]
    public static class Main
    {
        public const string version = "1.0.5.0";
        [QModPatch]
        public static void Load()
        {
            DrillableSulfur dSulfur = new DrillableSulfur();
            dSulfur.Patch();
        }
    }
}
