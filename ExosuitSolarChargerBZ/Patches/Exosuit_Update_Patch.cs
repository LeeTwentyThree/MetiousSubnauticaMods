using HarmonyLib;
using UnityEngine;
using static DayNightCycle;
namespace ExosuitSolarChargerBZ.Patches
{
    [HarmonyPatch(typeof(Exosuit), nameof(Exosuit.Update))]
    class Exosuit_Update_Patch
    {
        [HarmonyPrefix]
        static void Prefix(Exosuit __instance)
        {
            var count = __instance.modules.GetCount(Main.exosuitSolar.TechType);
            if (count > 0)
            {
                float depth = Mathf.Clamp01((200f + __instance.transform.position.y) / 200f);
                float light = main.GetLocalLightScalar();
                float amount = depth * light;

                __instance.AddEnergy(amount * main.deltaTime);
            }
        }
    }
}
