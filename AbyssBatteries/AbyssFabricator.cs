using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace AbyssBatteries
{
    internal class AbyssFabricator : CustomFabricator
    {
        private static Texture2D texture;
        public override Models Model { get; } = Models.Fabricator;
        public AbyssFabricator()
            : base("AbyssFabricator",
                  "Abyss Fabricator",
                  "Abyss Batteries Fabricator")
        {
            OnStartedPatching += () =>
            {
                texture = ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "AbyssFabricatorskin.png"));
            };
        }
        public override GameObject GetGameObject()
        {
            GameObject prefab = base.GetGameObject();
            if (texture != null)
            {
                SkinnedMeshRenderer skinnedMeshRenderer = prefab.GetComponentInChildren<SkinnedMeshRenderer>();
                skinnedMeshRenderer.material.mainTexture = texture;
            }
            return prefab;
        }
        protected override TechData GetBlueprintRecipe()
        {
            return new TechData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.Titanium, 2),
                    new Ingredient(TechType.Quartz, 2),
                    new Ingredient(TechType.JeweledDiskPiece, 1),
                }
            };
        }
        protected override Atlas.Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.Fabricator);
        }
        public override TechCategory CategoryForPDA { get; } = TechCategory.InteriorModule;
        public override TechGroup GroupForPDA { get; } = TechGroup.InteriorModules;
    }
}
