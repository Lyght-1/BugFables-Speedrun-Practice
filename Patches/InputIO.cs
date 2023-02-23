using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;
using UnityEngine;
using InputIOManager;
namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(InputIO), "SetDefaultKeys")]
    public class PatchInputIOSetDefaultKeys
    {
        static void Postfix(InputIO __instance)
        {
            InputIO.keys = new KeyCode[MainManager_Ext.BASE_KEY_AMOUNT+ Enum.GetValues(typeof(PracticeKeys)).Length];
            InputIO.keys[0] = KeyCode.UpArrow;
            InputIO.keys[1] = KeyCode.DownArrow;
            InputIO.keys[2] = KeyCode.LeftArrow;
            InputIO.keys[3] = KeyCode.RightArrow;
            InputIO.keys[4] = KeyCode.C;
            InputIO.keys[5] = KeyCode.X;
            InputIO.keys[6] = KeyCode.Z;
            InputIO.keys[7] = KeyCode.V;
            InputIO.keys[8] = KeyCode.Escape;
            InputIO.keys[9] = KeyCode.Return;
            InputIO.keys[10] = KeyCode.F1;
            InputIO.keys[11] = KeyCode.F2;
            InputIO.keys[12] = KeyCode.F3;
            InputIO.keys[13] = KeyCode.F4;
            InputIO.keys[14] = KeyCode.F5;
            InputIO.keys[15] = KeyCode.F6;
            InputIO.keys[16] = KeyCode.F7;
            InputIO.keys[17] = KeyCode.F8;
            InputIO.keys[18] = KeyCode.F9;
            InputIO.keys[19] = KeyCode.F10;
            InputIO.keys[20] = KeyCode.F11;
            InputIO.keys[21] = KeyCode.F12;
            InputIO.keys[22] = KeyCode.Delete;
            InputIO.keys[23] = KeyCode.LeftControl;
            InputIO.keys[24] = KeyCode.Minus;
            InputIO.keys[25] = KeyCode.Equals;
            InputIO.keys[26] = KeyCode.Alpha1;
            InputIO.keys[27] = KeyCode.Alpha2;
            InputIO.keys[28] = KeyCode.Alpha3;
            InputIO.keys[29] = KeyCode.Alpha4;
            InputIO.keys[30] = KeyCode.Alpha5;
            InputIO.keys[31] = KeyCode.Alpha6;
            InputIO.keys[32] = KeyCode.Alpha7;
            InputIO.keys[33] = KeyCode.Alpha8;
            InputIO.keys[34] = KeyCode.F3;
            InputIO.keys[35] = KeyCode.F4;
            InputIO.keys[36] = KeyCode.F5;
            InputIO.keys[37] = KeyCode.Alpha9;
        }
    }

    [HarmonyPatch(typeof(InputIO), "StartUp")]
    public class PatchInputIOStartUp
    {
        static void Postfix(InputIO __instance)
        {
            InputIO.bindingkeys.AddRange(new KeyCode[]
            {
                KeyCode.F1,
                KeyCode.F2,
                KeyCode.F3,
                KeyCode.F4,
                KeyCode.F5,
                KeyCode.F6,
                KeyCode.F7,
                KeyCode.F8,
                KeyCode.F9,
                KeyCode.F10,
                KeyCode.F11,
                KeyCode.F12
            });
        }
    }
}
