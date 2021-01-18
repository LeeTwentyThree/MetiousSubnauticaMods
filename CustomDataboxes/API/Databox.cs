using System;
using System.Collections.Generic;
using QModManager.Utility;
using CustomDataboxes.Databoxes;
namespace CustomDataboxes.API
{
    public class Databox
    {
        /// <summary>
        /// the ClassID for the Custom Databox (Required)
        /// </summary>
        public string DataboxID { get; set; }
        /// <summary>
        /// The big and primary Description of the Databox (Required)
        /// </summary>
        public string PrimaryDescription { get; set; }
        /// <summary>
        /// <para>the smaller Description of the databox which is normally below the
        /// <seealso cref="AlreadyUnlockedDescription"/> (Optional)</para>
        /// </summary>
        public string SecondaryDescription { get; set; }
        /// <summary>
        /// The TechType to get unlocked (Required)
        /// </summary>
        public TechType TechTypeToUnlock { get; set; }
        /// <summary>
        /// Biomes that the Databox would spawn in (Required)
        /// </summary>
        public List<LootDistributionData.BiomeData> BiomesToSpawnIn { get; set; }
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
                BiomesToSpawn = this.BiomesToSpawnIn,
            };
            dataBox.Patch();
        }
    }
}
