using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using UnityEngine;

namespace CameraDronePickup.Modules
{
    public class DronePickupModule : Equipable
    {
        public DronePickupModule()
            : base("DronePickupModule", "DronePickupModule", "module that makes me go yes"){}
        
        public override EquipmentType EquipmentType { get; } = EquipmentType.None;

        public override GameObject GetGameObject()
        {
            return CraftData.GetPrefabForTechType(TechType.MapRoomUpgradeScanRange);
        }

        public override CraftTree.Type FabricatorType { get; } = CraftTree.Type.MapRoom;
        protected override Atlas.Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.Titanium);
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 1,
                Ingredients = { new(TechType.Titanium, 1) }
            };
        }
    }
}