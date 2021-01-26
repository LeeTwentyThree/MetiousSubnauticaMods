using UnityEngine;
namespace LeviathanEggs.MonoBehaviours
{
    public class SpawnLocations : MonoBehaviour
    {
        public TechType techType;
        void Awake()
        {
            techType = gameObject.GetComponent<TechTag>().type;
        }
        void Start()
        {
            ErrorMessage.AddMessage($"{techType.AsString()} spawned in {gameObject.transform.position} coordinates.");
        }
    }
}
