using UnityEngine;

namespace AbyssBatteries.MonoBehaviours
{
    // this one is also yoinked from MrPurple6411, but i changed a bit so it can work for the Reactor Rod
    internal class PulsatingBehaviourRod : MonoBehaviour
    {
        Renderer[] renderers;

        private float currentStrength = 0;
        private float nextStrength = 2.5f;
        private float changeTime = 2f;
        private float timer = 0.0f;
        public void Awake()
        {
            renderers = gameObject.GetComponentsInChildren<Renderer>();
        }
        public void Update()
        {
            timer += Time.deltaTime;
            if (timer > changeTime)
            {
                currentStrength = nextStrength;
                nextStrength = currentStrength == 2.5f ? 0 : 2.5f;
                timer = 0.0f;
            }
            foreach (var renderer in renderers)
            {
                if (renderer != null)
                {
                    renderer.material.SetFloat(ShaderPropertyID._GlowStrength, Mathf.Lerp(currentStrength, nextStrength, timer / changeTime));
                    renderer.material.SetFloat(ShaderPropertyID._GlowStrengthNight, Mathf.Lerp(currentStrength, nextStrength, timer / changeTime));
                }
            }
        }
        public void OnDestroy()
        {
            foreach (var renderer in renderers)
            {
                renderer.material.SetFloat(ShaderPropertyID._GlowStrength, 2.5f);
                renderer.material.SetFloat(ShaderPropertyID._GlowStrengthNight, 2.5f);
            }
        }
    }
}
