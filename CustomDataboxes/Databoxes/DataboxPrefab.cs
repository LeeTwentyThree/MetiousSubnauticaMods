using System.Collections.Generic;
using SMLHelper.V2.Assets;
using UWE;
using UnityEngine;
namespace CustomDataboxes.Databoxes
{
    public class DataboxPrefab : Spawnable
    {
        private readonly string alreadyUnlockedTooltip;
        private readonly string primaryTooltip;
        private readonly string secondaryTooltip;
        private readonly TechType unlockTechType;
        private readonly List<LootDistributionData.BiomeData> biomesToSpawnIn;
        public DataboxPrefab(string classId, string alreadyUnlockedTooltip, string primaryTooltip, string secondaryTooltip, TechType unlockTechType, List<LootDistributionData.BiomeData> biomesToSpawnIn)
            : base(classId, classId, classId + " Databox")
        {
            this.alreadyUnlockedTooltip = alreadyUnlockedTooltip;
            this.primaryTooltip = primaryTooltip;
            this.secondaryTooltip = secondaryTooltip;
            this.unlockTechType = unlockTechType;
            this.biomesToSpawnIn = biomesToSpawnIn;
        }
        public override WorldEntityInfo EntityInfo => new WorldEntityInfo() { cellLevel = LargeWorldEntity.CellLevel.Medium, classId = ClassID, localScale = Vector3.one, prefabZUp = false, slotType = EntitySlot.Type.Medium, techType = this.TechType};
        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => this.biomesToSpawnIn;
        public override GameObject GetGameObject()
        {
            var path = "WorldEntities/Environment/DataBoxes/CompassDataBox";
            var prefab = Resources.Load<GameObject>(path);
            var obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            prefab.SetActive(false);
            obj.GetComponent<PrefabIdentifier>().ClassId = this.ClassID;
            obj.GetComponent<PrefabPlaceholdersGroup>();
            BlueprintHandTarget blueprintHandTarget = obj.GetComponent<BlueprintHandTarget>();
            blueprintHandTarget.alreadyUnlockedTooltip = this.alreadyUnlockedTooltip;
            blueprintHandTarget.primaryTooltip = this.primaryTooltip;
            blueprintHandTarget.secondaryTooltip = this.secondaryTooltip;
            blueprintHandTarget.unlockTechType = this.unlockTechType;

            obj.SetActive(true);
            return obj;
        }
    }
}
