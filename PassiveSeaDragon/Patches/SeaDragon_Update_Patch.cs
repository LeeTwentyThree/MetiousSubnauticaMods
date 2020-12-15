using HarmonyLib;
namespace PassiveSeaDragon.Patches
{
    [HarmonyPatch(typeof(SeaDragon), nameof(SeaDragon.Update))]
    internal class SeaDragon_Update_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(SeaDragon __instance)
        {
            __instance.Aggression.Value = 0.0f;
        }
    }
}
