using System.Collections.Generic;
using System.IO;
using ECCLibrary;
using UnityEngine;
using SMLHelper.V2.Utility;
namespace LeviathanEggs.Prefabs
{
    public class SeaEmperorEgg : CreatureEggAsset
    {
        // "WorldEntities/Eggs/EmperorEgg"
        // "WorldEntities/Doodads/Lost_river/Lost_river_tree_01"
        // "WorldEntities/Environment/Precursor/LostRiverBase/Precursor_LostRiverBase_SeaDragonEggShell"
        public SeaEmperorEgg()
            : base("SeaEmperorEgg", "Creature Egg", "An unknown Creature hatches from this", 
                  Resources.Load<GameObject>("WorldEntities/Eggs/EmperorEgg"),
                  TechType.SeaEmperorJuvenile, null, 1f)
        {
        }
        public override bool AcidImmune => true;
        public override bool IsScannable => true;
        public override string GetEncyDesc => "";
        public override string GetEncyTitle => "";
        public override string AssetsFolder => Main.AssetsFolder;
        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.PrisonAquarium_CaveFloor,
                count = 1,
                probability = 0.5f
            },
        };
        public override void AddCustomBehaviours()
        {
            GameObject.DestroyImmediate(prefab.GetComponent<IncubatorEgg>());
            //GameObject.DestroyImmediate(prefab.GetComponentInChildren<IncubatorEggAnimation>());

            prefab.GetComponent<Rigidbody>().isKinematic = false;
            prefab.GetComponent<Rigidbody>().mass = 100f;

            prefab.GetComponent<CreatureEgg>().animator = prefab.GetComponentInChildren<IncubatorEggAnimation>().eggAnimator;

            ResourceTracker resourceTracker = prefab.EnsureComponent<ResourceTracker>();
            resourceTracker.techType = this.TechType;
            resourceTracker.overrideTechType = TechType.GenericEgg;
        }
        public override Vector2int SizeInInventory => new Vector2int(3, 3);
        public override float GetMaxHealth => 60f;
        public override bool ManualEggExplosion => true;
        protected override Atlas.Sprite GetItemSprite()
        {
            return ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "SeaEmperorEgg.png"));
        }
    }
}
