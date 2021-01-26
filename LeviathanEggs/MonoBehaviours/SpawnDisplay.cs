using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace LeviathanEggs.MonoBehaviours
{
    public class SpawnDisplay : MonoBehaviour
    {
        void Update()
        {
            SpawnLocations[] gameObjects = FindObjectsOfType<SpawnLocations>();
            if (Input.GetKeyDown(KeyCode.K))
            {
                foreach (var obj in gameObjects)
                {
                    ErrorMessage.AddMessage($"{obj.techType.AsString()} location '{obj.gameObject.transform.position}'");
                }
            }
        }
    }
}
