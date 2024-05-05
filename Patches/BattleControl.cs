using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(BattleControl), "Update")]
    public class PatchBattleControlUpdate
    {
        static bool Prefix(BattleControl __instance)
        {
            if (__instance.GetComponent<BattleControl_Ext>() == null)
                MainManager.battle.gameObject.AddComponent<BattleControl_Ext>();

            if (!__instance.cancelupdate && MainManager.pausemenu == null && MainManager.instance.inbattle)
                __instance.GetComponent<BattleControl_Ext>().PracticeFKeys();

            if (MainManager_Ext.noFreezeRes)
            {
                if (MainManager.battle.enemydata != null)
                {
                    for (int i = 0; i != MainManager.battle.enemydata.Length; i++)
                    {
                        MainManager.battle.enemydata[i].freezeres = 0;
                    }
                }
            }

            if(MainManager_Ext.togglePerfectRNG && MainManager.instance.GetComponent<ILTimer>().il == IL.Astotheles)
            {
                if(__instance.enemydata != null && __instance.enemydata.Length > 0&& __instance.enemydata[0].animid == 40)
                {
                    __instance.enemydata[0].isdefending = false;
                    __instance.enemydata[0].defenseonhit = 0;
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(BattleControl), "ReturnToOverworld")]
    public class PatchBattleControlExitBattle
    {
        static bool Prefix(BattleControl __instance)
        {
            if (MainManager_Ext.ilMode)
            {
                var sdataRef = AccessTools.FieldRefAccess<BattleControl, BattleControl.StartUpData>("sdata");
                if (MainManager_Ext.ilMode)
                {
                    var ilTimer = MainManager.instance.GetComponent<ILTimer>();
                    MainManager.instance.StartCoroutine(ilTimer.WaitForEndOfBattle(sdataRef(__instance).enemies));
                }
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(BattleControl), "StartData")]
    public class PatchBattleControlStartData
    {
        static bool Prefix(BattleControl __instance, int[] enemyids)
        {
            if (ILTimer.start && MainManager_Ext.ilMode)
            {
                MainManager.instance.GetComponent<ILTimer>().CheckBattleSplit(enemies: enemyids, true);
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(BattleControl), "GetSingleTarget", new Type[] { })]
    public class PatchBattleControlGetSingleTarget
    {
        static bool Prefix(BattleControl __instance)
        {
            if (MainManager.battle.enemy && MainManager_Ext.togglePerfectRNG)
            {
                return BattleControl_Ext.CheckTarget();
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(BattleControl), "GetChance")]
    public class PatchBattleControlGetChance
    {
        static void Postfix(BattleControl __instance, int[] chances, ref int __result)
        {
            __result = BattleControl_Ext.CheckAttacks(__result);
        }
    }

    [HarmonyPatch(typeof(BattleControl), "DoAction")]
    public class PatchBattleControlDoAction
    {
        static IEnumerator AddPrefix(BattleControl __instance, IEnumerator __result)
        {
            var ilTimer = MainManager.instance.GetComponent<ILTimer>();
            if (BattleControl_Ext.currentActionID != -555)
            {
                ilTimer.timeAction = Time.realtimeSinceStartup;
            }
            while (__result.MoveNext())
            {
                yield return __result.Current;
            }

            if (MainManager_Ext.toggleActionTime && BattleControl_Ext.currentActionID != -555)
            {
                ilTimer.StartCoroutine(ilTimer.WaitForEndOfCommand());
            }
        }

        static void Postfix(BattleControl __instance, int actionid, ref IEnumerator __result)
        {
            BattleControl_Ext.currentActionID = actionid;
            Console.WriteLine("action id : " + BattleControl_Ext.currentActionID);
            __result = AddPrefix(__instance, __result);
        }
    }
}

