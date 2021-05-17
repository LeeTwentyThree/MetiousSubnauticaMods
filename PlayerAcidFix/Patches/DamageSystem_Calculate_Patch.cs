using HarmonyLib;
using UnityEngine;

namespace PlayerAcidFix.Patches
{
    [HarmonyPatch(typeof(DamageSystem), nameof(DamageSystem.CalculateDamage))]
    class DamageSystem_CalculateDamage_Patch
    {
        [HarmonyPostfix]
        public static float Postfix(float damage, DamageType type)
        {
            if (type == DamageType.Acid)
            {
                if (Player.main.GetVehicle() != null)
                {
                    if (Player.main.acidLoopingSound.playing)
                    {
                        Player.main.acidLoopingSound.Stop();
                    }
                    damage = 0f;
                }
            }

            return damage;
        }
    }
}
