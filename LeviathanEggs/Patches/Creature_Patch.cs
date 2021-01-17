using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;
namespace LeviathanEggs.Patches
{
    /*[HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
    class Creature_Patch
    {
        [HarmonyPrefix]
        static void Prefix(Creature __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);
            if (techType == TechType.SeaDragon)
            {
                var pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                pickupable.isPickupable = false;
                __instance.gameObject.EnsureComponent<WaterParkCreature>().pickupable = pickupable;
            }
            else if (techType == TechType.SeaEmperorJuvenile)
            {
                var pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                pickupable.isPickupable = false;
                __instance.gameObject.EnsureComponent<WaterParkCreature>().pickupable = pickupable;
            }
            else if (techType == TechType.SeaEmperor)
            {
                var pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                pickupable.isPickupable = false;
                __instance.gameObject.EnsureComponent<WaterParkCreature>().pickupable = pickupable;
            }
            else if (techType == TechType.SeaEmperorBaby)
            {
                var pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                pickupable.isPickupable = false;
                __instance.gameObject.EnsureComponent<WaterParkCreature>().pickupable = pickupable;
            }
        }
    }*/
}
