using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
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
        protected override Atlas.Sprite GetItemSprite() => new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("BioPlasmaMK2"));
        public override Vector2int SizeInInventory => new Vector2int(1, 1);
        public override GameObject GetGameObject()
        {
            GameObject prefab = Main.assetBundle.LoadAsset<GameObject>("BioPlasma.prefab");
            GameObject obj = Object.Instantiate(prefab);
            Material warperMaterial = null;
            GameObject warperPiece = Resources.Load<GameObject>("WorldEntities/Environment/Precursor/LostRiverBase/Precursor_LostRiverBase_WarperLab_Extras");

            Renderer[] aRenderers = warperPiece.GetComponentsInChildren<Renderer>();
            foreach (var rend in aRenderers)
            {
                if (rend.name.EndsWith("part_08"))
                {
                    warperMaterial = rend.material;
                    break;
                }
                if (warperMaterial != null)
                    break;
            }
            if (warperMaterial is null)
                QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Error, "Warper Material is null!", null, true);

            obj.EnsureComponent<TechTag>().type = this.TechType;
            obj.EnsureComponent<PrefabIdentifier>().classId = this.ClassID;
            obj.EnsureComponent<Pickupable>().isPickupable = true;

            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach(var rend in renderers)
            {
                rend.material.shader = Shader.Find("MarmosetUBER");
                rend.sharedMaterial.shader = Shader.Find("MarmosetUBER");

                rend.material = warperMaterial;
                rend.sharedMaterial = warperMaterial;
            }
            SkyApplier skyApplier = obj.EnsureComponent<SkyApplier>();
            skyApplier.anchorSky = Skies.Auto;
            skyApplier.renderers = renderers;
            skyApplier.enabled = true;

            obj.EnsureComponent<Rigidbody>();

            WorldForces wf = obj.EnsureComponent<WorldForces>();
            wf.aboveWaterGravity = 0f;
            wf.underwaterGravity = 0f;

            warperPiece.SetActive(false);
            prefab.SetActive(false);
            return obj;
        }
    }
}
