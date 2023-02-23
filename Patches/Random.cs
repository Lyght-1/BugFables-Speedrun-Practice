using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using System.Diagnostics;
namespace SpeedrunPractice.Patches
{
    //im desperate
    [HarmonyPatch(typeof(UnityEngine.Random), "Range", new Type[] { typeof(float), typeof(float) })]
    public class PatchRandomRangeFloat
    {
        static void Postfix(float min, float max, ref float __result)
        {
            __result = BattleControl_Ext.CheckRNG(min, max, __result);
        }
    }

    [HarmonyPatch(typeof(UnityEngine.Random), "Range", new Type[] { typeof(int), typeof(int) })]
    public class PatchRandomRangeInt
    {
        static void Postfix(int min, int max, ref int __result)
        {
            __result = (int)BattleControl_Ext.CheckRNG(min, max, __result);
        }
    }
}
