using HarmonyLib;
namespace AbyssBatteries.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.OnTakeDamage))]
    public static class Creature_OnTakeDamage
    {
        [HarmonyPostfix]
        public static void Postfix(Creature __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);
            if (techType == TechType.SpineEel)
            {
                if (!__instance.liveMixin.IsAlive())
                {
                    Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                    pickupable.isPickupable = true;
                    pickupable.randomizeRotationWhenDropped = true;
                }
            }
            else if (__instance.gameObject.TryGetComponent(out Pickupable pickupable))
            {
                pickupable.isPickupable = false;
            }
        }
    }
}
