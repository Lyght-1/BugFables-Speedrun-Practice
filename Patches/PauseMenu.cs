using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;

namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(PauseMenu), "Start")]
    public class PatchPauseMenuStart
    {
        static bool Prefix(PauseMenu __instance)
        {
            if (MainManager_Ext.ilMode)
            {
                var ilTimer = MainManager.instance.GetComponent<ILTimer>();
                ilTimer.timerUI.gameObject.SetActive(false);
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(PauseMenu), "DestroyPause")]
    public class PatchPauseMenuDestroyPause
    {
        static bool Prefix(PauseMenu __instance)
        {
            if (MainManager_Ext.ilMode)
            {
                var ilTimer = MainManager.instance.GetComponent<ILTimer>();
                ilTimer.timerUI.gameObject.SetActive(true);
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(PauseMenu), "Update")]
    public class PatchPauseMenuUpdate
    {
        static bool Prefix(PauseMenu __instance, ref int ___secondoption, ref int ___maxsecond)
        {
            bool canpick = (bool)typeof(PauseMenu).GetField("canpick", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            if (canpick && __instance.windowid == 3)
            {
                if (MainManager.instance.inputcooldown <= 0f)
                {
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        PauseMenu_Ext.checkingPBs = !PauseMenu_Ext.checkingPBs;
                        ___secondoption = PauseMenu_Ext.checkingPBs ? 0 : -1;
                        var updateTextRef = AccessTools.Method(typeof(PauseMenu), "UpdateText");
                        updateTextRef.Invoke(__instance, null);
                    }

                    if(MainManager.GetKey(5, true))
                    {
                        PauseMenu_Ext.checkingPBs = false;
                    }
                }

                if (PauseMenu_Ext.checkingPBs)
                {
                    ___maxsecond = PauseMenu_Ext.maxOption;
                }
            }
            return true;
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var instructionsList = instructions.ToList();
            var pressedResetInputKeysRef = AccessTools.Method(typeof(MainManager_Ext), "PressedResetInputKeys");
            int indexToInsertPatch = -1;
            for (int i = 0; i < instructionsList.Count; i++)
            {
                var inst = instructionsList[i];

                if (inst.LoadsConstant(282) && indexToInsertPatch == -1)
                {
                    indexToInsertPatch = i;
                    break;
                }
            }

            var label = instructionsList[indexToInsertPatch - 17].operand;
            instructionsList.RemoveRange(indexToInsertPatch, 2);
            instructionsList.Insert(indexToInsertPatch, new CodeInstruction(OpCodes.Call, pressedResetInputKeysRef).WithLabels((Label)label));
            return instructionsList;
        }
    }

    [HarmonyPatch(typeof(PauseMenu), "UpdateText")]
    public class PatchPauseMenuUpdateText
    {
        [Harmony]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var instructionsList = instructions.ToList();
            var setOptionsTextRef = AccessTools.Method(typeof(PauseMenu_Ext), "BuildPBWindow");
            int indexToInsertPatch = -1;
            for (int i = 0; i < instructionsList.Count; i++)
            {
                var inst = instructionsList[i];

                if (inst.operand == typeof(PauseMenu).GetField("maxsecond", BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    indexToInsertPatch = i;
                    break;
                }
            }

            var localList = instructionsList[indexToInsertPatch -4].operand;
            var instructionsToInsert = new List<CodeInstruction>();
            instructionsToInsert.Add(new CodeInstruction(OpCodes.Ldloc, localList));
            instructionsToInsert.Add(new CodeInstruction(OpCodes.Call, setOptionsTextRef));
            instructionsList.InsertRange(indexToInsertPatch, instructionsToInsert);
            return instructionsList;
        }
    }

    [HarmonyPatch(typeof(PauseMenu), "OnGUI")]
    public class PatchPauseMenuOnGUI
    {
        [Harmony]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var instructionsList = instructions.ToList();
            var playBuzzerRef = AccessTools.Method(typeof(MainManager), "PlayBuzzer");
            for (int i = 0; i < instructionsList.Count; i++)
            {
                var inst = instructionsList[i];

                if (inst.operand == playBuzzerRef && inst.opcode == OpCodes.Call)
                {
                    instructionsList[i].opcode = OpCodes.Nop;
                    instructionsList[i + 1].opcode = OpCodes.Ldc_I4_1;
                    break;
                }
            }
            return instructionsList;
        }
    }
}
