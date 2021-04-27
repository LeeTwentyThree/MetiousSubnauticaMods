using HarmonyLib;
using QModManager.API.ModLoading;
using System.Reflection;

namespace SeaMothAutomaticSonar
{
    [QModCore]
    public static class Main
    {
        static readonly Assembly myAssembly = Assembly.GetExecutingAssembly();

        public const string version = "1.0.1.0";

        [QModPatch]
        public static void Load()
        {
            CraftData.slotTypes[TechType.SeamothSonarModule] = QuickSlotType.Selectable;

            Harmony.CreateAndPatchAll(myAssembly, $"Metious_{myAssembly.GetName().Name}");
        }
    }
}