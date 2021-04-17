using HarmonyLib;

namespace CameraDronePickup.Patches
{
    [HarmonyPatch(typeof(MapRoomFunctionality))]
    public class MapRoomFunctionalityPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MapRoomFunctionality.IsAllowedToAdd))]
        static bool Prefix(ref bool __result, Pickupable pickupable)
        {
            var tt = pickupable.GetTechType();

            if (tt == Main.pickupModule.TechType)
            {
                __result = true;
                return false;
            }

            __result = false;
            return true;
        }
    }
}