using HarmonyLib;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(PDAScanner), nameof(PDAScanner.Unlock))]
    public static class PDAScanner_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(PDAScanner.EntryData entryData)
        {
            if (entryData.key == Main.seaDragonEgg.TechType)
                PDAEncyclopedia.Add("UnknownEgg", true);
            else if (entryData.key == Main.ghostEgg.TechType)
                PDAEncyclopedia.Add("UnknownEgg", true);
            else if (entryData.key == Main.seaEmperorEgg.TechType)
                PDAEncyclopedia.Add("UnknownEgg", true);

            return true;
        }
    }
}
