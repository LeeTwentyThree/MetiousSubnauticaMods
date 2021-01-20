using System.Collections;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using UnityEngine;
namespace ExosuitSolarChargerBZ.Modules
{
    public class ExosuitSolarChargerModule : Equipable
    {
        public override EquipmentType EquipmentType => EquipmentType.ExosuitModule;
        public override QuickSlotType QuickSlotType => QuickSlotType.Passive;
        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;
        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;
        public override TechType RequiredForUnlock => TechType.BaseUpgradeConsole;
        public ExosuitSolarChargerModule()
            : base("ExosuitSolarChargerModule", "PRAWN Suit Solar Charger Module", "a solar charger for the PRAWN Suit. (doesn't stack)")
        {
        }
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.ExoHullModule1);
            yield return task;
            GameObject prefab = task.GetResult();
            GameObject obj = GameObject.Instantiate(prefab);
            prefab.SetActive(false);

            gameObject.Set(obj);
        }
        protected override RecipeData GetBlueprintRecipe()
        {
            return new RecipeData()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.AdvancedWiringKit, 1),
                    new Ingredient(TechType.EnameledGlass, 1),
                }
            };
        }
        public override CraftTree.Type FabricatorType => CraftTree.Type.SeamothUpgrades;
        public override string[] StepsToFabricatorTab => new string[] { "ExosuitModules" };
        public override string AssetsFolder { get; } = Main.AssetsFolder;
    }
}
