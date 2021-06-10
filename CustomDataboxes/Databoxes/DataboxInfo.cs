using System.Collections.Generic;
using UnityEngine;

namespace CustomDataboxes.Databoxes
{
    internal class DataboxInfo
    {
        public string DataboxID { get; set; }
        public string AlreadyUnlockedDescription { get; set; }
        public string PrimaryDescription { get; set; }
        public string SecondaryDescription { get; set; }
        public string ItemToUnlock { get; set; }
        public List<LootDistributionData.BiomeData> BiomesToSpawnIn { get; set; }
        public List<Vector3> CoordinatedSpawns { get; set; }

    }
}
