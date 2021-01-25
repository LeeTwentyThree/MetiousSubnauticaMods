using UnityEngine;
namespace LeviathanEggs.MonoBehaviours
{
    public class SpawnLocations : MonoBehaviour
    {
        TechType techType;
        void Awake()
        {
            techType = gameObject.GetComponent<TechTag>().type;
        }
        void Start()
        {
            ErrorMessage.AddMessage($"{techType.AsString()} spawned in {gameObject.transform.position} coordinates.");
        }
        void Update()
        {
            SpawnLocations[] gameObjects = FindObjectsOfType<SpawnLocations>();
            if (Input.GetKeyDown(KeyCode.K))
            {
                foreach (var obj in gameObjects)
                {
                    ErrorMessage.AddMessage($"{techType.AsString()} location '{obj.gameObject.transform.position}'");
                }
            }
        }
    }
}
