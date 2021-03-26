using UnityEngine;

namespace SeaMothAutomaticSonar.MonoBehaviours
{
    class SeaMothSonarController : MonoBehaviour
    {
        public SeaMoth seaMoth;

        void OnEnable() => InvokeRepeating(nameof(SonarEffect), 0f, 5f);

        void OnDisable() => CancelInvoke(nameof(SonarEffect));

        void SonarEffect()
        {
            if (seaMoth is not null)
            {
                seaMoth.sonarSound.Stop();
                seaMoth.sonarSound.Play();
                SNCameraRoot.main.SonarPing();
            }
        }
    }
}
