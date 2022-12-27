using BepInEx;
using SMLHelper.V2.Handlers;
namespace CoffeeSoundFix
{
    [BepInPlugin("GUID HERE", "Coffee Sound Fix", "VERSION NUMBER HERE")]
    public class CoffeeSoundFix : BaseUnityPlugin
    {

        private void Start()
        {
            CraftDataHandler.SetEatingSound(TechType.Coffee, "event:/player/drink");
        }
    }
}
