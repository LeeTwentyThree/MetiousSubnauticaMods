using System.Collections.Generic;
namespace CustomDataboxes.Databoxes
{
    public class DataboxInfo
    {
        public string DataboxID { get; set; }
        public string AlreadyUnlockedDescription { get; set; }
        public string PrimaryDescription { get; set; }
        public string SecondaryDescription { get; set; }
        public TechType ItemToUnlock { get; set; }
        public List<LootDistributionData.BiomeData> BiomesToSpawnIn { get; set; }

    }
}
