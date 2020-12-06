using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UnityEngine;
namespace AbyssBatteries.Items
{
    class DragonScale : Spawnable
    {
        public static TechType TechTypeID { get; protected set; }
        public DragonScale()
            : base("DragonScale",
                  "Dragon Scale",
                  "this loose scale was collected from a Sea Dragon Leviathan's torso, where besides protecting the dragon from the extreme temeratures of the lava zone possesses unknown qualities. potential applications in advanced battery crafting detected.")
        {
            OnFinishedPatching += () =>
            {
                CraftDataHandler.SetHarvestOutput(TechType.SeaDragon, this.TechType);
                CraftDataHandler.SetHarvestType(TechType.SeaDragon, HarvestType.DamageAlive);
                CraftDataHandler.SetItemSize(this.TechType, 2, 2);
                TechTypeID = this.TechType;
            };
        }
        #region override GameObject
        public override GameObject GetGameObject()
        {
            var prefab = CraftData.GetPrefabForTechType(TechType.StalkerTooth);
            var obj = GameObject.Instantiate(prefab);
            prefab.SetActive(false);
            return obj;
        }
        #endregion
        protected override Atlas.Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.StalkerTooth);
        }
        //public override string AssetsFolder { get; } = Main.AssetsFolder;
    }
}
