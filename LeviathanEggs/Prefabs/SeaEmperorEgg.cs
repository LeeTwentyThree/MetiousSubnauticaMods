using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECCLibrary;
using UnityEngine;
using UWE;
using SMLHelper.V2.Utility;
namespace LeviathanEggs.Prefabs
{
    public class SeaEmperorEgg : CreatureEggAsset
    {
        // "WorldEntities/Eggs/EmperorEgg"
        // "WorldEntities/Doodads/Lost_river/Lost_river_tree_01"
        // "WorldEntities/Environment/Precursor/LostRiverBase/Precursor_LostRiverBase_SeaDragonEggShell"
        public SeaEmperorEgg(GameObject model)
            : base("SeaEmperorEgg", "SeaEmperor Egg", "SeaEmperor Egg that makes me go yes", model, TechType.SeaEmperorJuvenile, SpriteManager.Get(TechType.Aerogel).texture, 2f)
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
