using CameraDronePickup.MonoBehaviours;
using HarmonyLib;

namespace CameraDronePickup.Patches
{
    [HarmonyPatch(typeof(MapRoomCamera))]
    public class MapRoomCameraPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(MapRoomCamera.Start))]
        static void Postfix(MapRoomCamera __instance)
        {
            __instance.gameObject.EnsureComponent<Drone>();
            __instance.gameObject.EnsureComponent<DronePickup>();
        }
    }
}