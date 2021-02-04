using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace BioChemicalBatteries2.Prefabs
{
    class BioPlasma : Spawnable
    {
        public BioPlasma()
            : base("BioPlasmaMK2", "Warper Power Core", "A mysterious, powerful energy core harvested from a deceased Warper.")
        {
            OnFinishedPatching += () =>
            {
                CraftDataHandler.SetHarvestOutput(TechType.Warper, this.TechType);
                CraftDataHandler.SetHarvestType(TechType.Warper, HarvestType.DamageDead);
            };
        }
        protected override Atlas.Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.Titanium);
        }
        public override Vector2int SizeInInventory => new Vector2int(1, 1);
        public override GameObject GetGameObject()
        {
            GameObject prefab = Main.assetBundle.LoadAsset<GameObject>("BioPlasma.prefab");
            GameObject obj = Object.Instantiate(prefab);

            obj.EnsureComponent<TechTag>().type = this.TechType;
            obj.EnsureComponent<PrefabIdentifier>().classId = this.ClassID;
            obj.EnsureComponent<Pickupable>().isPickupable = true;

            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach(var rend in renderers)
            {
                foreach(var mat in rend.materials)
                {
                    mat.shader = Shader.Find("MarmosetUBER");
                    mat.mainTexture = Main.assetBundle.LoadAsset<Texture2D>("warper_entrails");
                    mat.SetTexture(ShaderPropertyID._SpecTex, Main.assetBundle.LoadAsset<Texture2D>("warper_entrails_spec"));
                    mat.SetTexture(ShaderPropertyID._BumpMap, Main.assetBundle.LoadAsset<Texture2D>("warper_entrails_normal"));
                    mat.SetColor(ShaderPropertyID._SpecColor, new Color(0.3f, 0.3f, 0.3f, 0.3f));

                    mat.EnableKeyword("MARMO_SPECMAP");
                    mat.EnableKeyword("MARMO_NORMALMAP");
                }
            }
            SkyApplier skyApplier = obj.EnsureComponent<SkyApplier>();
            skyApplier.anchorSky = Skies.Auto;
            skyApplier.renderers = renderers;
            skyApplier.enabled = true;

            obj.EnsureComponent<Rigidbody>();

            WorldForces wf = obj.EnsureComponent<WorldForces>();
            wf.aboveWaterGravity = 0f;
            wf.underwaterGravity = 0f;

            prefab.SetActive(false);
            return obj;
        }
    }
}
