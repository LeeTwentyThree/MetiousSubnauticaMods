using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace Common.Utility
{
    public static class MaterialUtils
    {
        public static void ApplyTextures(GameObject gameObject, Texture2D mainTexture)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    material.mainTexture = mainTexture;
                }
            }
        }
        public static void ApplyTextures(GameObject gameObject, Texture2D mainTexture, Texture2D specTexture)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    material.mainTexture = mainTexture;
                    material.SetTexture(ShaderPropertyID._SpecTex, specTexture);
                }
            }
        }
        public static void ApplyTextures(GameObject gameObject, Texture2D mainTexture, Texture2D specTexture, Texture2D illumTexture)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    material.mainTexture = mainTexture;
                    material.SetTexture(ShaderPropertyID._SpecTex, specTexture);
                    material.SetTexture(ShaderPropertyID._Illum, illumTexture);
                }
            }
        }
        public static void ApplyTexturesForMesh(GameObject gameObject, Texture2D mainTexture, Texture2D specTexture, Texture2D illumTexture, string meshName)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (var renderer in renderers)
            {
                if (renderer.name == meshName)
                {
                    renderer.material.mainTexture = mainTexture;
                    renderer.sharedMaterial.mainTexture = mainTexture;

                    renderer.material.SetTexture(ShaderPropertyID._SpecTex, specTexture);
                    renderer.sharedMaterial.SetTexture(ShaderPropertyID._SpecTex, specTexture);

                    renderer.material.SetTexture(ShaderPropertyID._Illum, illumTexture);
                    renderer.sharedMaterial.SetTexture(ShaderPropertyID._Illum, illumTexture);
                }
            }
        }
    }
}
