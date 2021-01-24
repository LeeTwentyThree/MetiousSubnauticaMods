using HarmonyLib;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
    class Creature_Start
    {
        [HarmonyPostfix]
        static void Postfix(Creature __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);
            foreach (TechType tt in Main.TechTypesToSkyApply)
            {
                if (techType == tt)
                {
                    SkyApplier skyApplier = __instance.gameObject.GetComponent<SkyApplier>();
                    skyApplier.anchorSky = Skies.Auto;
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
}
