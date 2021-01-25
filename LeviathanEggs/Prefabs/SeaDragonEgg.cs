using UnityEngine;
using ECCLibrary;
using SMLHelper.V2.Utility;
using UWE;
using System.Collections.Generic;
using System.IO;
using LeviathanEggs.MonoBehaviours;
namespace LeviathanEggs.Prefabs
{
    class SeaDragonEgg : CreatureEggAsset
    {
        public SeaDragonEgg()
            :base("SeaDragonEgg", "Creature Egg", "An unknown Creature hatches from this", 
                 Main.assetBundle.LoadAsset<GameObject>("SeaDragonEgg.prefab"),
                 TechType.SeaDragon, null, 1f)
        {
            
        }
        public override bool AcidImmune => true;
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
            GameObject SeaDragonEgg = Resources.Load<GameObject>("WorldEntities/Environment/Precursor/LostRiverBase/Precursor_LostRiverBase_SeaDragonEggShell");
            Renderer[] aRenderers = SeaDragonEgg.GetComponentsInChildren<Renderer>();
            Material shell = null;
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (var renderer in aRenderers)
            {
                if (renderer.name.StartsWith("Creatures_eggs_17"))
                {
                    shell = renderer.material;
                    break;
                }
                if (shell != null)
                    break;
            }
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                renderer.material.shader = shader;

                renderer.material = shell;
                renderer.material = shell;
            }
            SeaDragonEgg.SetActive(false);
            prefab.GetComponent<Rigidbody>().mass = 100f;

            ResourceTracker resourceTracker = prefab.EnsureComponent<ResourceTracker>();
            resourceTracker.techType = this.TechType;
            resourceTracker.overrideTechType = TechType.GenericEgg;
            resourceTracker.rb = prefab.GetComponent<Rigidbody>();
            resourceTracker.prefabIdentifier = prefab.GetComponent<PrefabIdentifier>();
            resourceTracker.pickupable = prefab.GetComponent<Pickupable>();

            prefab.AddComponent<SpawnLocations>();
        }
        public override Vector2int SizeInInventory => new Vector2int(3, 3);
        public override float GetMaxHealth => 60f;
        protected override Atlas.Sprite GetItemSprite()
        {
            return ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "SeaDragonEgg.png"));
        }
    }
}
