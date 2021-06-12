using System;
using System.Collections.Generic;
using CustomDataboxes.Databoxes;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace CustomDataboxes.API
{
    public class Databox
    {
        /// <summary>
        /// the ClassID for the Custom Databox.
        /// </summary>
        public string DataboxID { get; set; }
        
        /// <summary>
        /// The big and primary Description of the Databox.
        /// </summary>
        public string PrimaryDescription { get; set; }
        
        /// <summary>
        /// <para>the smaller Description of the databox which is normally below the
        /// <seealso cref="PrimaryDescription"/>.</para>
        /// </summary>
        public string SecondaryDescription { get; set; }
        
        /// <summary>
        /// The TechType to get unlocked.
        /// </summary>
        public TechType TechTypeToUnlock { get; set; }
        
        /// <summary>
        /// Biomes that the Databox would spawn in
        /// </summary>
        public List<LootDistributionData.BiomeData> BiomesToSpawnIn { get; set; }
        
        /// <summary>
        /// Coordinated (<see cref="Vector3"/>) Spawns for the Databox.
        /// </summary>
        public Dictionary<Vector3, Vector3> CoordinatedSpawns { get; set; }
        
        /// <summary>
        /// To edit the Databox's Game Object.
        /// </summary>
        public Action<GameObject> ModifyGameObject { get; set; }

        /// <summary>
        /// the TechType reference of this Databox. please keep in mind that this is always set to <see cref="TechType.None"/> if used before calling the <see cref="Patch"/> method.
        /// </summary>
        public TechType TechType { get; private set; }
        
        /// <summary>
        /// To patch and create the Databox.
        /// </summary>
        public void Patch()
        {
            string name = this.GetType().Assembly.GetName().Name;
            Logger.Log(Logger.Level.Info, $"Recieved Custom databox from '{name}'");

            string result = "";

            if (string.IsNullOrEmpty(DataboxID))
                result += "Missing required Info 'DataboxID'\n";
            if (string.IsNullOrEmpty(PrimaryDescription))
                result += "Missing required Info 'PrimaryDescription'\n";
            if (!string.IsNullOrEmpty(result))
            {
                string msg = "Unable to patch\n" + result;
                Logger.Log(Logger.Level.Error, msg);
                throw new InvalidOperationException(msg);
            }

            var dataBox = new CustomDatabox(DataboxID)
            {
                PrimaryDescription = this.PrimaryDescription,
                SecondaryDescription = this.SecondaryDescription,
                TechTypeToUnlock = this.TechTypeToUnlock,
                BiomesToSpawn = BiomesToSpawnIn,
                Vector3Spawns = CoordinatedSpawns,
                ModifyGameObject = this.ModifyGameObject
            };
            dataBox.Patch();

            TechType = dataBox.TechType;
        }
    }
}
