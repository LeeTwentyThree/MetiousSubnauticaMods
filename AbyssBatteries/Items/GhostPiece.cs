namespace AbyssBatteries.Items
{
    using SMLHelper.V2.Assets;
    using SMLHelper.V2.Handlers;
    using UnityEngine;
    class GhostPiece : Spawnable
    {
        public static TechType TechTypeID { get; protected set; }
        public GhostPiece()
            : base("GhostPiece", "Electrical Ghost Node", "This strange, pulsating, prong-like node was harvested from a Ghost Leviathan's body. can be used in advanced battery fabrication")
        {
            OnFinishedPatching += () =>
            {
                CraftDataHandler.SetHarvestOutput(TechType.GhostLeviathanJuvenile, this.TechType);
                CraftDataHandler.SetHarvestType(TechType.GhostLeviathanJuvenile, HarvestType.DamageAlive);
                CraftDataHandler.SetHarvestOutput(TechType.GhostLeviathan, this.TechType);
                CraftDataHandler.SetHarvestType(TechType.GhostLeviathan, HarvestType.DamageAlive);
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
        public override string AssetsFolder { get; } = Main.AssetsFolder;
    }
}