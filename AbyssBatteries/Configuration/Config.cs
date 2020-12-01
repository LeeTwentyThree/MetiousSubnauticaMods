using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
namespace AbyssBatteries.Configuration
{
    [Menu("Abyss Batteries", LoadOn = MenuAttribute.LoadEvents.MenuOpened | MenuAttribute.LoadEvents.MenuRegistered)]
    public class Config : ConfigFile
    {
        [Toggle("Batteries Complement Chain")]
        public bool Complement = false;
    }
    public class Values : ConfigFile
    {
        public Values() : base("values") { }

        public int JellyBatteryEnergy = 250;
        public int PoopBatteryEnergy = 500;
        public int AmpSquidBatteryEnergy = 740;
        public int FossilBatteryEnergy = 800;
        public int GhostBatteryEnergy = 8400;
        public int ThermalBatteryEnergy = 10540;
    }
}
