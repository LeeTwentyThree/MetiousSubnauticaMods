using System;
using System.Diagnostics;
using UnityEngine;

namespace CameraDronePickup.MonoBehaviours
{
    public class DronePickup : MonoBehaviour
    {
        Drone _drone;
        void Start()
        {
            _drone = GetComponent<Drone>();
        }

        void Update()
        {
            if (GameInput.GetButtonDown(GameInput.Button.LeftHand))
            {
                Use();
            }
        }

        void Use()
        {
            if (_drone.GetLastDockedOnMapRoom().storageContainer.container.GetCount(Main.pickupModule.TechType) > 0)
            {
                Pickupable pickupable = null;
                if (_drone.GetActiveTarget() is not null)
                {
                    pickupable = _drone.GetActiveTarget().GetComponent<Pickupable>();
                }

                if (pickupable is not null && pickupable.isPickupable)
                {
                    if (Inventory.Get().container.HasRoomFor(pickupable))
                    {
                        OnPickup();
                    }
                }
            }
        }

        public void OnPickup()
        {
            if (_drone.GetActiveTarget() is not null)
            {
                var pickupable = _drone.GetActiveTarget().GetComponent<Pickupable>();
                var pickPrefab = _drone.GetActiveTarget().GetComponent<PickPrefab>();

                if (pickupable is not null && pickupable.isPickupable && Inventory.Get().container.HasRoomFor(pickupable))
                {
                    pickupable = pickupable.Initialize();

                    var item = new InventoryItem(pickupable);
                    
                    Inventory.Get().container.UnsafeAdd(item);
                    return;
                }

                if (pickPrefab is not null && pickPrefab.AddToContainer(Inventory.Get().container))
                {
                    pickPrefab.SetPickedUp();
                }
            }
        }
    }
}