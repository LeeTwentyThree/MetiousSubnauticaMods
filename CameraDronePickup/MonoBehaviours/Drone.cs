using System;
using UnityEngine;

namespace CameraDronePickup.MonoBehaviours
{
    public class Drone : MonoBehaviour
    {
        GameObject _activeTarget;
        MapRoomCamera _camera;
        MapRoomCameraDocking _lastDock;

        void Start() => _camera = GetComponent<MapRoomCamera>();
        
        void Update()
        {
            UpdateLastDock();
            UpdateActiveTarget();
        }
        
        public MapRoomFunctionality GetLastDockedOnMapRoom()
        {
            return _lastDock.GetComponentInParent<MapRoomFunctionality>();
        }

        public GameObject GetActiveTarget() => _activeTarget;

        void UpdateLastDock()
        {
            if (_camera.dockingPoint is not null)
            {
                _lastDock = _camera.dockingPoint;
            }
        }
        
        void UpdateActiveTarget()
        {
            if (GetLastDockedOnMapRoom().storageContainer.container.GetCount(Main.pickupModule.TechType) > 0)
            {
                Targeting.GetTarget(gameObject, 7f, out GameObject target, out float distance, null);

                var root = UWE.Utils.GetEntityRoot(target);
                root = root != null ? root : target;

                if (root.GetComponentProfiled<Pickupable>() is not null &&
                    CraftData.GetTechType(root) != TechType.MapRoomCamera)
                {
                    target = root;
                }
                else
                {
                    target = null;
                }

                _activeTarget = target;

                var guiHand = Player.main.GetComponent<GUIHand>();
                if (_activeTarget is not null)
                    GUIHand.Send(_activeTarget, HandTargetEventType.Hover, guiHand);
            }
        }
    }
}