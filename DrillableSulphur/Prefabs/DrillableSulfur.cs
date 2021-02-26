using System;
using System.Collections.Generic;
using UWE;
using static LootDistributionData;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Collections;

namespace DrillableSulphur.Prefabs
{
    class DrillableSulfur : Spawnable
    {
        public DrillableSulfur()
            :base("DrillableSulfur",
                 "Drillable Sulfur",
                 "Drillable Sulfur")
        {
        }
        public override List<BiomeData> BiomesToSpawnIn => new List<BiomeData>()
        {
                new BiomeData()
                {
                    biome = BiomeType.BonesField_LakePit_Floor,
                    count = 1,
                    probability = 0.3f
                },
                new BiomeData()
                {
                    biome = BiomeType.BonesField_Lake_Floor,
                    count = 1,
                    probability = 0.075f
                },
                new BiomeData()
                {
                    biome = BiomeType.LostRiverJunction_LakeFloor,
                    count = 1,
                    probability = 0.05f
                },
                new BiomeData()
                {
                    biome = BiomeType.InactiveLavaZone_LavaPit_Floor,
                    count = 1,
                    probability = 0.05f
                },
                new BiomeData()
                {
                    biome = BiomeType.InactiveLavaZone_Chamber_Floor_Far,
                    count = 1,
                    probability = 0.01f
                },
                new BiomeData()
                {
                    biome = BiomeType.ActiveLavaZone_Chamber_Floor,
                    count = 1,
                    probability = 0.2f
                }
        };
        public override WorldEntityInfo EntityInfo => new WorldEntityInfo() { cellLevel = LargeWorldEntity.CellLevel.Medium, classId = this.ClassID,
            localScale = Vector3.one, prefabZUp = false, slotType = EntitySlot.Type.Medium, techType = this.TechType };
        public override GameObject GetGameObject()
        {
            GameObject prefab = Resources.Load<GameObject>("WorldEntities/Natural/drillable/DrillableSulphur");
            prefab.SetActive(false);
            GameObject obj = GameObject.Instantiate(prefab);

            obj.EnsureComponent<TechTag>().type = this.TechType;
            obj.EnsureComponent<PrefabIdentifier>().classId = this.ClassID;
            obj.EnsureComponent<SkyApplier>().enabled = true;
            ResourceTracker rt = obj.EnsureComponent<ResourceTracker>();
            rt.overrideTechType = TechType.Sulphur;
            rt.techType = TechType.Sulphur;

            obj.SetActive(true);
            return obj;
        }
    }
}
