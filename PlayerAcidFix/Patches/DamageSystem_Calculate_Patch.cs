using HarmonyLib;
using UnityEngine;

namespace PlayerAcidFix.Patches
{
    [HarmonyPatch(typeof(DamageSystem), nameof(DamageSystem.CalculateDamage))]
    class DamageSystem_CalculateDamage_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(ref float __result, DamageType type)
        {
            if (type == DamageType.Acid)
            {
                if (Player.main.GetVehicle() != null)
                {
                    if (Player.main.acidLoopingSound.playing)
                    {
                        Player.main.acidLoopingSound.Stop();
                    }
                    __result = 0f;
                }
            }
        }
    }
}
