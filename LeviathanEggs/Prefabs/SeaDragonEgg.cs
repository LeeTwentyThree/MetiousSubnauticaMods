using UnityEngine;
using ECCLibrary;
using SMLHelper.V2.Utility;
using UWE;
using System.Collections.Generic;

namespace LeviathanEggs.Prefabs
{
    class SeaDragonEgg : CreatureEggAsset
    {
        public SeaDragonEgg(GameObject model)
            :base("SeaDragonEgg", "SeaDragon Egg", "SeaDragon Egg that makes you go yes", model, TechType.SeaDragon, SpriteManager.Get(TechType.SeaDragon).texture, 2f)
        {
        }
        public override bool AcidImmune => true;
        public override string AssetsFolder => Main.AssetsFolder;
        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SafeShallows_Grass,
                count = 4,
                probability = 1f
            }
        };
        public override Vector2int SizeInInventory => new Vector2int(2, 2);
        public override float GetMaxHealth => 60f;
        public override bool ManualEggExplosion => true;
    }
}
