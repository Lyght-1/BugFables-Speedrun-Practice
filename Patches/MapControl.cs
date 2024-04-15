using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;

namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(MapControl), "Start")]
    public class PatchMapControlStart
    {
        static void Postfix(MapControl __instance)
        {
            if (__instance.mapid == MainManager.Maps.UndergroundBar)
                MainManager.UpdateShops();
        }
    }
}
