using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
namespace AbyssBatteries.Configuration
{
    [Menu("Abyss Batteries", LoadOn = MenuAttribute.LoadEvents.MenuOpened | MenuAttribute.LoadEvents.MenuRegistered)]
    public class Config : ConfigFile
    {
        [Toggle("Complement Chain (restart may required)")]
        public bool Complement = false;
    }
    public class Values : ConfigFile
    {
        public Values() : base("values") { }

        public int JellyBatteryEnergy = 300;
        public int PoopBatteryEnergy = 720;
        public int AmpSquidBatteryEnergy = 1105;
        public int FossilBatteryEnergy = 1250;
        public int GhostBatteryEnergy = 7540;
        public int ThermalBatteryEnergy = 10540;
    }
}
