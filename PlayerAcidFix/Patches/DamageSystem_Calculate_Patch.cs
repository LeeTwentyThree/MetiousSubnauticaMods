using HarmonyLib;
using UnityEngine;

namespace PlayerAcidFix.Patches
{
    [HarmonyPatch(typeof(DamageSystem), nameof(DamageSystem.CalculateDamage))]
    class DamageSystem_CalculateDamage_Patch
    {
        [HarmonyPostfix]
        public static float Postfix(float damage, DamageType type, GameObject target)
        {
            if (target == Player.main.gameObject)
            {
                damage *= DamageSystem.damageMultiplier;
                DamageModifier[] componentsInChildren = target.GetComponentsInChildren<DamageModifier>();
                for (int i = 0; i < componentsInChildren.Length; i++)
                {
                    damage = componentsInChildren[i].ModifyDamage(damage, type);
                }
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
            }

            return damage;
        }
    }
}
