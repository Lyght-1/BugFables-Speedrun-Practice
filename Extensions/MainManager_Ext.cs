using UnityEngine;
using SpeedrunPractice.Extensions;
using InputIOManager;
using HarmonyLib;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

namespace SpeedrunPractice.Extensions
{
    public enum NewListType
    {
        IndividualLevel=36
    }
    public enum PracticeKeys
    {
        DebugMenu=10, InputDisplay, Heal, InfJump, Speed, Save, Reload, MainMenu, DownPos, UpPos, SavePos, LoadPos,
        ToggleCollision, FreeCam, TextStorage,AntiSoftlock, ExitIL, MapList, ChooseIL, ResetIL, ToggleGhost, UndoSplit, FreezeResistance, HideTimer,
        KillEnemies,FleeBattle,ReloadBattle,PerfectRNG,ActionTime,IcefallVisualizer,PerfectIcefallToggle
    }
    public class MainManager_Ext : MonoBehaviour
    {
        public static bool drawInfo = false;
        public static bool toggleInfJump = false;
        public static int pp_TeleportIndex = 0;
        public static bool toggleActionTime = false;
        public static Vector3[] pp_TeleportArray = null;
        public static bool showInputDisplay = false;
        public static bool battleMenu = false;
        public static bool toggleCollision = false;
        public static bool toggleFreeCam = false;
        public static bool toggleTextStorage = false;
        public static bool togglePerfectRNG = false;
        public static bool ilMode = false;
        public static bool noFreezeRes = false;
        public static bool isLoading = false;
        public static float flyHoldFrames = 0;
        public static string[] practiceKeyText =
        {
            "Debug Menu",
            "Input Display",
            "Heal Party",
            "Infinite Jump",
            "Double Speed",
            "Save Game",
            "Reload Save",
            "Back to Main Menu",
            "Select Down Position",
            "Select Up Position",
            "Save Position",
            "Load Position",
            "Toggle Collision",
            "Free Cam",
            "Text Storage",
            "Anti Softlock",
            "Exit IL",
            "Map List",
            "Choose IL",
            "Reset IL",
            "Toggle Ghost",
            "Undo Split",
            "Toggle Freeze Resistance",
            "Hide Timer IL",
            "Kill Enemies",
            "Flee Battle",
            "Reload Battle",
            "Toggle Perfect RNG",
            "Toggle Action Time",
            "Toggle Icefall Visualizer",
            "Perfect Icefalls Check"
        };
        public const int BASE_KEY_AMOUNT = 10;
        public static void ResetState()
        {
            drawInfo = false;
            toggleInfJump = false;
            pp_TeleportIndex = 0;
            pp_TeleportArray = new Vector3[5];
            showInputDisplay = false;
            battleMenu = false;
            toggleCollision = false;
            toggleFreeCam = false;
            toggleTextStorage = false;
            togglePerfectRNG = false;
            PlayerControl_Ext.speed = 5;
            if (ilMode)
            {
                MainManager.instance.GetComponent<ILTimer>().ExitILMode();
            }
        }

        void Update()
        {
            if (toggleFreeCam)
            {
                MainManager.instance.hudcooldown = 0;
                MainManager.instance.showmoney = 0;
            }
        }

        public static string GetPracticeKeyText(int num)
        {
            string label = num <= BASE_KEY_AMOUNT-1 ? MainManager.menutext[num + 88] : practiceKeyText[num-BASE_KEY_AMOUNT];
            return "|button," + num + ",0|" + label;
        }
            
        public static string GetILText(int num)
        {
            var ILs = Enum.GetValues(typeof(IL)).Cast<IL>().Where(a => a != IL.None).ToList();
            return Enum.GetNames(typeof(IL))[MainManager.listvar[num]].ToString();
        }

        public static bool CheckListType(int type)
        {
            if(type == (int)NewListType.IndividualLevel)
            {
                var ILs = Enum.GetValues(typeof(IL)).Cast<IL>().Where(a => a != IL.None).ToList();
                MainManager.listvar = MainManager.GradualFill(ILs.Count);
                return true;
            }
            return false;
        }

        public static bool PressedResetInputKeys()
        {
            return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKey(KeyCode.F1);
        }
    }
}
