using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UnityEngine;
namespace AbyssBatteries.Items
{
    class ReefbackEnzyme : Spawnable
    {
        public static TechType TechTypeID { get; protected set; }
        public ReefbackEnzyme()
            :base("ReefbackEnzyme",
                 "Reefback Enzyme",
                 "this enzyme sample was extracted from one of the various pods found on a reefback leviathan, where is serves an unknown purpose in its digestive system. possible applications in advanced battery crafting detected")
        {
            OnFinishedPatching += () =>
            {
                CraftDataHandler.SetHarvestOutput(TechType.Reefback, this.TechType);
                CraftDataHandler.SetHarvestType(TechType.Reefback, HarvestType.DamageAlive);
                CraftDataHandler.SetHarvestOutput(TechType.ReefbackBaby, this.TechType);
                CraftDataHandler.SetHarvestType(TechType.ReefbackBaby, HarvestType.DamageAlive);
                CraftDataHandler.SetItemSize(this.TechType, 2, 2);
                TechTypeID = this.TechType;
            };

        }
        #region override GameObject
        public override GameObject GetGameObject()
        {
            var prefab = CraftData.GetPrefabForTechType(TechType.HatchingEnzymes);
            var obj = GameObject.Instantiate(prefab);
            prefab.SetActive(false);
            return obj;
        }
        #endregion
        //public override string AssetsFolder { get; } = Main.AssetsFolder;
        protected override Atlas.Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.HatchingEnzymes);
        }
    }
}
