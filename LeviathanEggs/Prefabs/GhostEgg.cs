using System.Collections.Generic;
using System.Globalization;
using UWE;
using SMLHelper.V2.Utility;
using ECCLibrary;
using UnityEngine;
namespace LeviathanEggs.Prefabs
{
    class GhostEgg : CreatureEggAsset
    {
        public GhostEgg()
            : base("GhostEgg", "Ghost Leviathan Egg", "Ghost Leviathan Egg that makes me go yes", 
                  Resources.Load<GameObject>("WorldEntities/Doodads/Lost_river/lost_river_cove_tree_01"),
                  TechType.GhostLeviathanJuvenile, SpriteManager.Get(TechType.Titanium).texture, 2f)
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
        public override void AddCustomBehaviours()
        {
            GameObject model = prefab.FindChild("lost_river_cove_tree_01");

            model.transform.localScale *= 0.08f;

            model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x + 75f, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z + 15f);

            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.02f, model.transform.localPosition.z - 1.6f);

            GameObject.DestroyImmediate(prefab.GetComponent<ConstructionObstacle>());

            foreach (Transform transform in prefab.transform)
            {
                if (string.Compare(transform.name, "lost_river_cove_tree_01", true, CultureInfo.InvariantCulture) != 0)
                    GameObject.DestroyImmediate(transform);
                else
                    foreach (Transform tr in transform)
                        if (!tr.name.StartsWith("lost_river_cove_tree_01_eggs", true, CultureInfo.InvariantCulture))
                            GameObject.DestroyImmediate(tr);
            }
            Renderer[] renderers = prefab.GetAllComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
                if (!renderer.name.StartsWith("lost_river_cove_tree_01_eggs", true, CultureInfo.InvariantCulture))
                    renderer.enabled = false;

            Collider[] colliders = prefab.GetAllComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
                GameObject.DestroyImmediate(colliders[i]);

            BoxCollider box = prefab.AddComponent<BoxCollider>();
            box.size = new Vector3(1f, 0.8f, 1f);
            box.center = new Vector3(box.center.x, box.center.y + 0.4f, box.center.z + 0.3f);

            SkyApplier skyApplier = prefab.GetComponent<SkyApplier>() ?? prefab.GetComponentInChildren<SkyApplier>();
            skyApplier.anchorSky = Skies.Auto;
            skyApplier.dynamic = false;
            skyApplier.emissiveFromPower = false;
            skyApplier.hideFlags = HideFlags.None;
            skyApplier.enabled = true;

            prefab.EnsureComponent<Animator>();
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
