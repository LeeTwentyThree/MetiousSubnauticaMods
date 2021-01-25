using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UWE;
using SMLHelper.V2.Utility;
using ECCLibrary;
using UnityEngine;
using System;
using LeviathanEggs.MonoBehaviours;
namespace LeviathanEggs.Prefabs
{
    class GhostEgg : CreatureEggAsset
    {
        public GhostEgg()
            : base("GhostEgg", "Creature Egg", "An unknown Creature hatches from this",
                  Main.assetBundle.LoadAsset<GameObject>("GhostEgg.prefab"),
                  TechType.GhostLeviathanJuvenile, null, 1f)
        {
        }
        public override bool AcidImmune => true;
        public override string AssetsFolder => Main.AssetsFolder;
        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.TreeCove_LakeFloor,
                count = 1,
                probability = 0.2f,
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.TreeCove_Ground,
                count = 1,
                probability = 0.4f
            }
        };
        public override Vector2int SizeInInventory => new Vector2int(3, 3);
        public override float GetMaxHealth => 60f;
        public override bool ManualEggExplosion => false;
        public override void AddCustomBehaviours()
        {
            GameObject ghostEgg = Resources.Load<GameObject>("WorldEntities/Doodads/Lost_river/lost_river_cove_tree_01");
            Renderer[] aRenderer = ghostEgg.GetAllComponentsInChildren<Renderer>();
            Material shell = null;
            Material embryo = null;
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (var renderer in aRenderer)
            {
                if (renderer.name == "lost_river_cove_tree_01_eggs_shells")
                {
                    shell = renderer.material;
                    break;
                }
                if (shell != null)
                    break;
            }
            foreach (var renderer in aRenderer)
            {
                if (renderer.name == "lost_river_cove_tree_01_eggs")
                {
                    embryo = renderer.material;
                    break;
                }
                if (embryo != null)
                    break;
            }
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            Material[] materials = new Material[2] { shell, embryo };
            foreach(var renderer in renderers)
            {
                if (shell != null && embryo != null)
                {
                    renderer.material.shader = shader;
                    renderer.materials = materials;
                }
            }
            if (renderers == null)
            {
                ErrorMessage.AddMessage("renderer is null");
                Console.WriteLine("Renderer is null");
            }
            if (shell == null)
            {
                ErrorMessage.AddMessage("shell material is null");
                Console.WriteLine("Shell material is null");
            }
            if (embryo == null)
            {
                ErrorMessage.AddMessage("embryo material is null");
                Console.WriteLine("Embryo material is null");
            }
            ghostEgg.SetActive(false);

            prefab.GetComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

            prefab.GetComponent<Rigidbody>().mass = 100f;

            ResourceTracker resourceTracker = prefab.EnsureComponent<ResourceTracker>();
            resourceTracker.techType = this.TechType;
            resourceTracker.overrideTechType = TechType.GenericEgg;
            resourceTracker.rb = prefab.GetComponent<Rigidbody>();
            resourceTracker.prefabIdentifier = prefab.GetComponent<PrefabIdentifier>();
            resourceTracker.pickupable = prefab.GetComponent<Pickupable>();

            prefab.AddComponent<SpawnLocations>();
        }
        protected override Atlas.Sprite GetItemSprite()
        {
            return ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "GhostEgg.png"));
        }
        /*private static GameObject GetGhostEgg()
        {
            var model = Resources.Load<GameObject>("WorldEntities/Doodads/Lost_river/lost_river_cove_tree_01");
            var obj = GameObject.Instantiate(model);

            GameObject tree = obj.FindChild("lost_river_cove_tree_01");

            GameObject.DestroyImmediate(obj.GetComponent<ConstructionObstacle>());

            foreach (Transform transform in tree.transform)
            {
                if (string.Compare(transform.name, "lost_river_cove_tree_01", true, CultureInfo.InvariantCulture) != 0)
                    GameObject.DestroyImmediate(transform);
                else
                    foreach (Transform tr in transform)
                        if (!tr.name.StartsWith("lost_river_cove_tree_01_eggs", true, CultureInfo.InvariantCulture))
                            GameObject.DestroyImmediate(tr);
            }
            Renderer[] renderers = obj.GetAllComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
                if (!renderer.name.StartsWith("lost_river_cove_tree_01_eggs", true, CultureInfo.InvariantCulture))
                    renderer.enabled = false;

            Collider[] colliders = obj.GetAllComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
                GameObject.DestroyImmediate(colliders[i]);

            BoxCollider box = obj.AddComponent<BoxCollider>();
            box.size = new Vector3(1f, 0.8f, 1f);
            box.center = new Vector3(box.center.x, box.center.y + 0.4f, box.center.z + 0.3f);

            model.SetActive(false);

            return obj;
        }*/
    }
}
