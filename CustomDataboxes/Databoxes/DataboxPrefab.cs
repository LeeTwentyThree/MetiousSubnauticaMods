using System.Collections.Generic;
using SMLHelper.V2.Assets;
using UWE;
using UnityEngine;
using System.Collections;

namespace CustomDataboxes.Databoxes
{
    internal class DataboxPrefab : Spawnable
    {
        readonly string alreadyUnlockedTooltip;
        readonly string primaryTooltip;
        readonly string secondaryTooltip;
        readonly TechType unlockTechType;
        readonly List<LootDistributionData.BiomeData> biomesToSpawnIn;
        readonly List<Spawnable.SpawnLocation> coordinatedSpawns;

        public DataboxPrefab(string classId, string alreadyUnlockedTooltip, string primaryTooltip,
            string secondaryTooltip, TechType unlockTechType, List<LootDistributionData.BiomeData> biomesToSpawnIn,
            List<Spawnable.SpawnLocation> coordinatedSpawns)
            : base(classId, classId, classId + " Databox")
        {
            this.alreadyUnlockedTooltip = alreadyUnlockedTooltip;
            this.primaryTooltip = primaryTooltip;
            this.secondaryTooltip = secondaryTooltip;
            this.unlockTechType = unlockTechType;
            this.biomesToSpawnIn = biomesToSpawnIn;
            this.coordinatedSpawns = coordinatedSpawns;
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo() { cellLevel = LargeWorldEntity.CellLevel.Medium, classId = ClassID, localScale = Vector3.one, prefabZUp = false, slotType = EntitySlot.Type.Medium, techType = this.TechType};
        
        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => this.biomesToSpawnIn;

        public override List<Spawnable.SpawnLocation> CoordinatedSpawns => coordinatedSpawns;
        
#if SN1
        public override GameObject GetGameObject()
        {
            var path = "WorldEntities/Environment/DataBoxes/CompassDataBox";
            var prefab = Resources.Load<GameObject>(path);
            var obj = GameObject.Instantiate(prefab);
            obj.name = ClassID;
            obj.SetActive(false);
            prefab.SetActive(false);
            obj.GetComponent<PrefabIdentifier>().ClassId = this.ClassID;

            BlueprintHandTarget blueprintHandTarget = obj.GetComponent<BlueprintHandTarget>();
            blueprintHandTarget.alreadyUnlockedTooltip = this.alreadyUnlockedTooltip;
            blueprintHandTarget.primaryTooltip = this.primaryTooltip;
            blueprintHandTarget.secondaryTooltip = this.secondaryTooltip;
            blueprintHandTarget.unlockTechType = this.unlockTechType;

            obj.SetActive(true);
            return obj;
        }
#elif BZ
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabForFilenameAsync("WorldEntities/Alterra/DataBoxes/BeaconDataBox.prefab");
            yield return request;
            request.TryGetPrefab(out GameObject prefab);

            GameObject obj = GameObject.Instantiate(prefab);
            obj.name = ClassID;
            obj.SetActive(false);

            obj.GetComponent<PrefabIdentifier>().classId = this.ClassID;

            BlueprintHandTarget blueprintHandTarget = obj.GetComponent<BlueprintHandTarget>();
            blueprintHandTarget.alreadyUnlockedTooltip = this.alreadyUnlockedTooltip;
            blueprintHandTarget.primaryTooltip = this.primaryTooltip;
            blueprintHandTarget.secondaryTooltip = this.secondaryTooltip;
            blueprintHandTarget.unlockTechType = this.unlockTechType;

            obj.SetActive(true);

            gameObject.Set(obj);
        }
#endif
    }
}
