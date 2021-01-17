using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWE;
using SMLHelper.V2.Utility;
using ECCLibrary;
using UnityEngine;
namespace LeviathanEggs.Prefabs
{
    class GhostLeviathanEgg : CreatureEggAsset
    {
        public GhostLeviathanEgg(GameObject model)
            : base("GhostLeviathanEgg", "Ghost Leviathan Egg", "Ghost Leviathan Egg that makes me go yes", model, TechType.GhostLeviathanJuvenile, SpriteManager.Get(TechType.Titanium).texture, 2f)
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
