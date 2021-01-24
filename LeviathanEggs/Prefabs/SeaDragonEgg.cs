using UnityEngine;
using ECCLibrary;
using SMLHelper.V2.Utility;
using UWE;
using System.Collections.Generic;
using System.IO;
namespace LeviathanEggs.Prefabs
{
    class SeaDragonEgg : CreatureEggAsset
    {
        public SeaDragonEgg()
            :base("SeaDragonEgg", "Creature Egg", "An unknown Creature hatches from this", 
                 Resources.Load<GameObject>("WorldEntities/Environment/Precursor/LostRiverBase/Precursor_LostRiverBase_SeaDragonEggShell"),
                 TechType.SeaDragon, null, 1f)
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
                biome = BiomeType.InactiveLavaZone_Chamber_Ceiling,
                count = 1,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.InactiveLavaZone_Chamber_Lava,
                count = 1,
                probability = 0.25f,
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.ActiveLavaZone_Chamber_Floor,
                count = 1,
                probability = 0.15f
            }
        };
        public override void AddCustomBehaviours()
        {
            prefab.GetComponent<Rigidbody>().isKinematic = false;
            prefab.GetComponent<Rigidbody>().mass = 100f;

            GameObject.DestroyImmediate(prefab.GetComponent<ImmuneToPropulsioncannon>());

            var model = prefab.FindChild("Creatures_eggs_17");

            model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x - 90f, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z);

            model.transform.localScale *= 0.8f;

            CapsuleCollider capsule = prefab.GetComponentInChildren<CapsuleCollider>();
            capsule.radius *= 0.5f;
            capsule.height *= 0.5f;

            ResourceTracker resourceTracker = prefab.EnsureComponent<ResourceTracker>();
            resourceTracker.techType = this.TechType;
            resourceTracker.overrideTechType = TechType.GenericEgg;
        }
        public override Vector2int SizeInInventory => new Vector2int(3, 3);
        public override float GetMaxHealth => 60f;
        public override bool ManualEggExplosion => true;
        protected override Atlas.Sprite GetItemSprite()
        {
            return ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "SeaDragonEgg.png"));
        }
    }
}
