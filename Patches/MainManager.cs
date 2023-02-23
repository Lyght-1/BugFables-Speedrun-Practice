using HarmonyLib;
using SpeedrunPractice.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using InputIOManager;

namespace SpeedrunPractice.Patches
{

    [HarmonyPatch(typeof(MainManager), "UpdateJounal", new Type[] { typeof(MainManager.Library), typeof(int) })]
    public class PatchMainManagerUpdateJournal
    {
        static bool Prefix(MainManager __instance, MainManager.Library type, int variable)
        {
            if (MainManager.instance.discoverymessage.childCount == 5)
                MainManager.instance.discoverymessage.GetChild(4).gameObject.SetActive(false);
            return true;
        }
    }
    [HarmonyPatch(typeof(MainManager), "Start")]
    public class PatchMainManagerStart
    {
        static bool Prefix(MainManager __instance)
        {
            __instance.gameObject.AddComponent<MainManager_Ext>();
            return true;
        }

        static void Postfix(MainManager __instance)
        {
            MainManager.MainCamera.gameObject.AddComponent<FreeCam>();
        }
    }

    [HarmonyPatch(typeof(MainManager), "RefreshCamera")]
    public class PatchMainManagerRefreshCamera
    {
        static bool Prefix()
        {
            return !MainManager_Ext.toggleFreeCam;
        }
    }

    [HarmonyPatch(typeof(MainManager), "FadeIn", new Type[] { typeof(float), typeof(Color)})]
    public class PatchMainManagerFadeIn
    {
        static void Prefix(float speed, Color color)
        {
            if (ILTimer.lvlSongStarted)
            {
                ILTimer.lvlSongStarted = false;
                ILTimer.endCredit = true;
                MainManager.instance.StartCoroutine(MainManager.instance.GetComponent<ILTimer>().WaitForEndFade());
            }
        }
    }

    [HarmonyPatch(typeof(MainManager), "Reset")]
    public class PatchMainManagerReset
    {
        static void Prefix() => MainManager_Ext.ResetState();
    }

    [HarmonyPatch(typeof(MainManager), "SetVariables")]
    public class PatchMainManagerSetVariables
    {
        static void Postfix(MainManager __instance)
        {
            List<string> commonCopy = new List<string>(MainManager.commondialogue);
            commonCopy.Add("|transfer,var0,0,1,0|");
            commonCopy.Add("|event,505|");//choose IL
            commonCopy.Add("|event,506|");//save gold after reset event
            commonCopy.Add("|event,507|");//erase gold after reset event
            MainManager.commondialogue = commonCopy.ToArray();
            __instance.gameObject.AddComponent<ILTimer>();

            Directory.CreateDirectory("BepInEx/splits");
            Directory.CreateDirectory("BepInEx/ghostRecordings");
            MainManager.menutext = MainManager.menutext.Select(s => s.Contains("F1") ? s.Replace("F1", "Shift+F1") : s).ToArray();
        }
    }

    [HarmonyPatch(typeof(MainManager), "LoadMap", new Type[] { typeof(int) })]
    public class PatchMainManagerLoadMap
    {
        static void Postfix(MainManager __instance, int id)
        {
            if (MainManager_Ext.ilMode)
            {
                if (!ILTimer.start)
                {
                    MainManager.instance.GetComponent<ILTimer>().CheckStart(id);
                }
                else
                {
                    MainManager.instance.GetComponent<ILTimer>().CheckSplit(id);
                }
            }
        }
    }

    [HarmonyPatch(typeof(MainManager), "SaveSettings")]
    public class PatchMainManagerSaveSettings
    {
        static void Postfix(MainManager __instance, ref string __result)
        {
            var textList = __result.Split('\n').ToList();

            TextWriter textWriter = File.CreateText("practiceKeys.dat");
            textWriter.Write(string.Join("\n",textList.GetRange(MainManager_Ext.BASE_KEY_AMOUNT, InputIO.keys.Length- MainManager_Ext.BASE_KEY_AMOUNT).ToArray()));
            textWriter.Close();

            textList.RemoveRange(MainManager_Ext.BASE_KEY_AMOUNT, InputIO.keys.Length - MainManager_Ext.BASE_KEY_AMOUNT);
            __result = string.Join("\n",textList.ToArray());
        }
    }

    [HarmonyPatch(typeof(MainManager), "ReadSettings")]
    public class PatchMainManagerReadSettings
    {
        static int keyLenght = -1;
        static void Prefix(MainManager __instance)
        {
            keyLenght = InputIO.keys.Length;

            var keyList = InputIO.keys.ToList();
            keyList.RemoveRange(MainManager_Ext.BASE_KEY_AMOUNT, InputIO.keys.Length - MainManager_Ext.BASE_KEY_AMOUNT);
            InputIO.keys = keyList.ToArray();
        }

        static void Postfix(MainManager __instance)
        {
            if (File.Exists("practiceKeys.dat"))
            {
                Array.Resize(ref InputIO.keys, keyLenght);
                Console.WriteLine("File practice keys Exist");
                string[] practiceKeyText = File.ReadAllLines("practiceKeys.dat");
                for (int i = MainManager_Ext.BASE_KEY_AMOUNT; i != InputIO.keys.Length; i++)
                {
                    InputIO.keys[i] = (KeyCode)Enum.Parse(typeof(KeyCode), practiceKeyText[i - MainManager_Ext.BASE_KEY_AMOUNT]);
                }
            }
            else
            {
                InputIO.SetDefaultKeys();
                Console.WriteLine("file practice keys doesnt exist sadge");
            }
        }
    }

    [HarmonyPatch(typeof(MainManager), "RandomSort", new Type[] { typeof(List<int>) }, new ArgumentType[]{ArgumentType.Ref})]
    public class PatchMainManagerRandomSort
    {
        static void Postfix(MainManager __instance, ref List<int> array)
        {
            if (MainManager_Ext.ilMode && ILTimer.start)
            {
                var ilTimer = MainManager.instance.GetComponent<ILTimer>();
                if (ilTimer.il == IL.EnterBH && array.Contains((int)MainManager.BadgeTypes.PowerExchange))
                {
                    array.Remove((int)MainManager.BadgeTypes.PowerExchange);
                    array.Insert(1, (int)MainManager.BadgeTypes.PowerExchange);
                }else if((ilTimer.il == IL.EnterFG || ilTimer.il == IL.MerchantsRescue) && array.Contains((int)MainManager.BadgeTypes.PoisonAttacker))
                {
                    array.Remove((int)MainManager.BadgeTypes.PoisonAttacker);
                    array.Insert(1, (int)MainManager.BadgeTypes.PoisonAttacker);
                }
            }
        }
    }

    [HarmonyPatch(typeof(MainManager), "ShowItemList")]
    public class PatchMainManagerShowItemList
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var instructionsList = instructions.ToList();
            var getPracticeKeyTextRef = AccessTools.Method(typeof(MainManager_Ext), "GetPracticeKeyText");
            var getILTextRef = AccessTools.Method(typeof(MainManager_Ext), "GetILText");
            var checkListTypeRef = AccessTools.Method(typeof(MainManager_Ext), "CheckListType");
            int indexToInsertPatch = -1;
            int indexLocalInt = -1;
            int indexToInsertNewListType = -1;
            int indexToSetListVar = -1;
            int indexMaxOptions = -1;
            for (int i = 0; i < instructionsList.Count; i++)
            {
                var inst = instructionsList[i];

                if (inst.LoadsConstant(88) && indexToInsertPatch == -1)
                {
                    indexToInsertPatch = i+6;
                    indexLocalInt = i - 1;
                }

                if(inst.operand as string == "Data/Dialogues")
                {
                    indexToInsertNewListType = i - 3;
                }

                if(inst.opcode == OpCodes.Stloc_2 && indexToSetListVar == -1)
                {
                    indexToSetListVar = i + 1;
                }

                if (inst.operand == typeof(MainManager).GetField("maxoptions") && indexMaxOptions == -1)
                {
                    indexMaxOptions = i -5;
                }
            }

            var localText = instructionsList[indexToInsertPatch -1];
            var localInt = instructionsList[indexLocalInt];
            var instructionsGetPracticeKeyText = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldloc_S, localInt.operand),
                new CodeInstruction(OpCodes.Call, getPracticeKeyTextRef),
                new CodeInstruction(OpCodes.Stloc_S, localText.operand),
            };

            var lbl = generator.DefineLabel();
            instructionsList[indexToInsertNewListType].labels.Add(lbl);
            var instructionsIlList = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldc_I4_S, (int)NewListType.IndividualLevel),
                new CodeInstruction(OpCodes.Bne_Un, lbl),
                new CodeInstruction(OpCodes.Ldloc_S, localInt.operand),
                new CodeInstruction(OpCodes.Call, getILTextRef),
                new CodeInstruction(OpCodes.Stloc_S, localText.operand),
            };

            var instructionsInsertType = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, checkListTypeRef),
                new CodeInstruction(OpCodes.Brtrue, instructionsList[indexMaxOptions].operand),
            };

            instructionsList.InsertRange(indexToInsertPatch, instructionsGetPracticeKeyText);
            instructionsList.InsertRange(indexToInsertNewListType, instructionsIlList);
            instructionsList.InsertRange(indexToSetListVar, instructionsInsertType);
            return instructionsList;
        }
    }
}
