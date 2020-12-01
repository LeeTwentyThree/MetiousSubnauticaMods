using AbyssBatteries.Items;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SMLHelper.V2.Utility;
using AbyssBatteries.MonoBehaviours;
using SMLHelper.V2.Handlers;
using System;

namespace AbyssBatteries.Craftables
{
    class GhostDNA : Craftable
    {
        public static TechType TechTypeID { get; protected set; }
        private static Texture2D illumTexture;
        private static Texture2D texture;
        private static Texture2D normalTexture;
        private static Texture2D specTexture;
        public override TechType RequiredForUnlock => GhostPiece.TechTypeID;
        public GhostDNA()
            : base("GhostDNA", "Hypercharged Ghost Plasma", "Very powerful and volatile substance extracted from Ghost Nodes")
        {

            OnStartedPatching += () =>
            {
                illumTexture = ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "GhostDNAillum.png"));
                texture = ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "GhostDNAskin.png"));
                normalTexture = ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "GhostDNAnormal.png"));
                specTexture = ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "GhostDNAspec.png"));

            };
            OnFinishedPatching += () =>
            {
                TechTypeID = this.TechType;
                CraftDataHandler.SetCraftingTime(this.TechType, 3f);
            };
        }
        #region override GameObject
        public override GameObject GetGameObject()
        {
            var prefab = CraftData.GetPrefabForTechType(TechType.ReactorRod);
            var obj = GameObject.Instantiate(prefab);
            GameObject aquarium = Resources.Load<GameObject>("Submarine/Build/Aquarium");
            #region glass material
            Material glass = null;
            Renderer[] aRenderers = aquarium.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer aRenderer in aRenderers)
            {
                foreach (Material aMaterial in aRenderer.materials)
                {
                    if (aMaterial.name.StartsWith("Aquarium_glass", StringComparison.OrdinalIgnoreCase))
                    {
                        glass = aMaterial;
                        break;
                    }
                }
                if (glass != null)
                    break;
            }
            #endregion
            Shader shader = Shader.Find("MarmosetUBER");
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                if (renderer.name == "nuclear_reactor_rod_mesh")
                {

                    renderer.sharedMaterial.shader = shader;
                    renderer.material.shader = shader;

                    renderer.sharedMaterial.mainTexture = texture;
                    renderer.material.mainTexture = texture;

                    renderer.sharedMaterial.SetTexture("_BumpMap", normalTexture);
                    renderer.material.SetTexture("_BumpMap", normalTexture);

                    renderer.sharedMaterial.SetTexture("_SpecTex", specTexture);
                    renderer.material.SetTexture("_SpecTex", specTexture);

                    renderer.sharedMaterial.SetTexture("_Illum", illumTexture);
                    renderer.material.SetTexture("_Illum", illumTexture);

                    obj.EnsureComponent<PulsatingBehaviourRod>();

                    renderer.sharedMaterial.EnableKeyword("MARMO_NORMALMAP");
                    renderer.sharedMaterial.EnableKeyword("MARMO_EMISSION");
                    renderer.sharedMaterial.EnableKeyword("MARMO_SPECMAP");
                    renderer.material.EnableKeyword("MARMO_NORMALMAP");
                    renderer.material.EnableKeyword("MARMO_EMISSION");
                    renderer.material.EnableKeyword("MARMO_SPECMAP");

                    renderer.sharedMaterial.SetFloat("_GlowStrength", 1.5f);
                    renderer.material.SetFloat("_GlowStrength", 1.5f);
                }
                #region nuclear_reactor_rod_glass
                else if (renderer.name == "nuclear_reactor_rod_glass")
                {
                    renderer.material = glass;
                    renderer.sharedMaterial.SetColor("_Color", new Color(1f, 1f, 1f, 0.87f));
                    renderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.87f));
                }
                #endregion
            }
            prefab.SetActive(false);
            return obj;
        }
        #endregion
        protected override TechData GetBlueprintRecipe()
        {
            return new TechData
            {
                craftAmount = 2,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(GhostPiece.TechTypeID,1)
                },
            };
        }
        public override TechGroup GroupForPDA { get; } = TechGroup.Resources;
        public override TechCategory CategoryForPDA { get; } = TechCategory.Electronics;
        public override string AssetsFolder { get; } = Main.AssetsFolder;
    }
}