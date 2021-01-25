using HarmonyLib;
using UnityEngine;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
    class Creature_Start
    {
        [HarmonyPostfix]
        static void Postfix(Creature __instance)
        {
            if (__instance.gameObject.transform.position == Vector3.zero)
                GameObject.DestroyImmediate(__instance.gameObject);

            TechType techType = CraftData.GetTechType(__instance.gameObject);
            foreach (TechType tt in Main.TechTypesToSkyApply)
            {
                if (techType == tt)
                {
                    GameObject.DestroyImmediate(__instance.gameObject.GetComponent<SkyApplier>());

                    SkyApplier skyApplier = __instance.gameObject.AddComponent<SkyApplier>();

                    if (__instance.gameObject.TryGetComponent(out WaterParkCreature waterParkCreature) && waterParkCreature.IsInsideWaterPark())
                        skyApplier.anchorSky = Skies.BaseInterior;
                    else
                        skyApplier.anchorSky = Skies.Auto;
                    skyApplier.dynamic = false;
                    skyApplier.emissiveFromPower = false;
                    skyApplier.hideFlags = HideFlags.None;
                    skyApplier.enabled = true;
                }
            }
            foreach (TechType tt in Main.TechTypesToMakePickupable)
            {
                if (techType == tt)
                {
                    Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                    pickupable.isPickupable = false;
                }
            }
        }
    }
    [HarmonyPatch(typeof(Creature), nameof(Creature.OnTakeDamage))]
    static class Creature_OnTakeDamage_Patch
    {
        [HarmonyPostfix]
        static void Postfix(Creature __instance, ref DamageInfo damageInfo)
        {
            if (__instance.TryGetComponent(out WaterParkCreature waterParkCreature) && waterParkCreature.IsInsideWaterPark())
            {
                ErrorMessage.AddMessage($"Creature {__instance.gameObject.GetComponent<TechTag>().type.AsString()} is damaged with amount: '{damageInfo.damage}' by dealer: '{damageInfo.dealer.name}' in the ACU.");
            }
        }
    }
}
