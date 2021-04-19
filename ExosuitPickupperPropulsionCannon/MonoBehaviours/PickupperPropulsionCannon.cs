using System;
using UnityEngine;

namespace ExosuitPickupperPropulsionCannon.MonoBehaviours
{
    public class PickupperPropulsionCannon : MonoBehaviour
    {
        GameObject _activeTarget;
        PropulsionCannon _propulsionCannon;
        Exosuit _exosuit;

        Exosuit exosuit 
        {
            get 
            {
                if (_exosuit is null)
                    return GetComponentInParent<Exosuit>();
                    
                return _exosuit;
            }
        }

        void Start()
        {
            _propulsionCannon = GetComponent<PropulsionCannon>();
            _exosuit = GetComponentInParent<Exosuit>();
        }

        void Update()
        {
            UpdateActiveTarget();

            if (_activeTarget is not null && GameInput.GetButtonDown(GameInput.Button.Deconstruct))
                Pickup();
        }

        void UpdateActiveTarget()
        {
            if (_propulsionCannon is not null && _propulsionCannon.IsGrabbingObject())
            {
                _activeTarget = _propulsionCannon.GetNearbyGrabbedObject();
            }
        }

        void Pickup()
        {
            if (_activeTarget is not null)
            {
                var pickupable = _activeTarget.GetComponent<Pickupable>();

                if (pickupable is not null && pickupable.isPickupable && exosuit.storageContainer.container.HasRoomFor(pickupable))
                {
                    uGUI_IconNotifier.main.Play(pickupable.GetTechType(), uGUI_IconNotifier.AnimationType.From);
                    
                    pickupable = pickupable.Initialize();

                    var item = new InventoryItem(pickupable);
                    exosuit.storageContainer.container.UnsafeAdd(item);
                    pickupable.PlayPickupSound();
                }
            }
        }
    }
}
