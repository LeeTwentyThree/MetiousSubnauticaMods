using AbyssBatteries.MonoBehaviours;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using static Charger;

namespace AbyssBatteries.Patches
{
    // this patch is yoinked from MrPurple6411
    [HarmonyPatch(typeof(Charger), nameof(Charger.OnEquip))]
    internal static class Charger_OnEquip_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(Charger __instance, string slot, InventoryItem item, Dictionary<string, SlotDefinition> ___slots)
        {
            if (___slots.TryGetValue(slot, out SlotDefinition slotDefinition))
            {
                GameObject battery = slotDefinition.battery;
                Pickupable pickupable = item?.item;
                if (battery != null && pickupable != null && Main.abyssBatteries.Contains(pickupable.GetTechType()))
                {
                    battery.EnsureComponent<PulsatingBehaviour>(); // Ensure the Pulsating Behaviour when the battery is in a charger
                }
                else if (battery != null && battery.TryGetComponent(out PulsatingBehaviour pulsatingBehaviour))
                {
                    GameObject.Destroy(pulsatingBehaviour); 
                }
            }
        }
    }
}
