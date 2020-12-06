using HarmonyLib;
using AbyssBatteries.MonoBehaviours;
using System.Collections.Generic;
using UnityEngine;
using static EnergyMixin;

namespace AbyssBatteries.Patches
{
    [HarmonyPatch(typeof(EnergyMixin), nameof(EnergyMixin.NotifyHasBattery))]
    public static class EnergyMixin_NotifyHasBattery_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(EnergyMixin __instance, InventoryItem item)
        {
            if (item != null)
            {
                TechType techType = item.item.GetTechType();

                if (Main.abyssBatteries.Contains(techType))
                {
                    foreach (BatteryModels batteryModel in __instance.batteryModels)
                    {
                        if (batteryModel.techType == techType)
                        {
                            batteryModel.model.EnsureComponent<PulsatingBehaviour>();
                            return;
                        }
                    }

                    __instance.gameObject.EnsureComponent<PulsatingBehaviour>();
                    return;
                }
            }

            if (__instance.TryGetComponent(out PulsatingBehaviour pulsatingBehaviour))
                GameObject.Destroy(pulsatingBehaviour);
        }
    }
}
