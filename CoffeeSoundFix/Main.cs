using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
namespace CoffeeSoundFix
{
    [QModCore]
    public static class Main
    {
        [QModPatch]
        public static void Load() => CraftDataHandler.SetEatingSound(TechType.Coffee, "event:/player/drink");
    }
}
