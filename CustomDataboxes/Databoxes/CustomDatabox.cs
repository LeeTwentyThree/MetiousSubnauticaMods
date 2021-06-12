using System;
using System.Collections.Generic;
using System.Collections;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UWE;
using UnityEngine;
namespace CustomDataboxes.Databoxes
{
    internal class CustomDatabox : Spawnable
    {
        public string PrimaryDescription { get; set; }
        
        public string SecondaryDescription { get; set; }
        
        public TechType TechTypeToUnlock { get; set; }

        public List<LootDistributionData.BiomeData> BiomesToSpawn { get; set; }
        
        public Dictionary<Vector3, Vector3> Vector3Spawns { get; set; }
        
        public Action<GameObject> ModifyGameObject { get; set; }
        
        public CustomDatabox(string databoxID)
            :base(databoxID, databoxID ,databoxID) => OnFinishedPatching += AddCoordinatedSpawns;

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo() { cellLevel = LargeWorldEntity.CellLevel.Medium, classId = ClassID, localScale = Vector3.one, prefabZUp = false, slotType = EntitySlot.Type.Medium, techType = this.TechType };

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => this.BiomesToSpawn;

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
            blueprintHandTarget.primaryTooltip = PrimaryDescription;
            blueprintHandTarget.secondaryTooltip = SecondaryDescription ?? PrimaryDescription;
            blueprintHandTarget.unlockTechType = TechTypeToUnlock;
            
            ModifyGameObject?.Invoke(obj);

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
            blueprintHandTarget.primaryTooltip = PrimaryDescription;
            blueprintHandTarget.secondaryTooltip = SecondaryDescription ?? PrimaryDescription;
            blueprintHandTarget.unlockTechType = TechTypeToUnlock;

            ModifyGameObject?.Invoke(obj);

            obj.SetActive(true);

            gameObject.Set(obj);
        }
#endif

        void AddCoordinatedSpawns()
        {
            if (Vector3Spawns is null)
                return;

            var spawns = new List<SpawnInfo>();
            foreach (var spawn in Vector3Spawns)
            {
                spawns.Add(new SpawnInfo(TechType, spawn.Key, Quaternion.Euler(spawn.Value)));
            }
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawns(spawns);
        }
    }

}
