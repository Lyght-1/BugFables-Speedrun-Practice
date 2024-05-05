using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using InputIOManager;
using System.Collections.Generic;
namespace SpeedrunPractice.Extensions
{
    public class PlayerControl_Ext : MonoBehaviour
    {
        public int guiInfoCount = 0;
        public string guiInfoMessage = "";
        public int pdllMenuCursorPos = 0;
        public static int speed = 5;
        public static int soundIndex = 0;
        public float startHeight = 0f;
        public void PracticeFKeys(PlayerControl __instance)
        {
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.ExitIL]))
            {
                if (MainManager_Ext.ilMode)
                {
                    MainManager.instance.GetComponent<ILTimer>().ExitILMode();
                }
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.MapList]))
            {
                StartCoroutine(MainManager.SetText("Maps|pickitem,31,0,false,false,-204,-11|", null, null));
                MainManager.instance.message = true;
            }

            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.ChooseIL]))
            {
                StartCoroutine(MainManager.SetText("Choose a Level |pickitem,36,0,false,false,-205,-11|", null, null));
                MainManager.instance.message = true;
            }

            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.DebugMenu]))
            {
                MainManager_Ext.drawInfo = !MainManager_Ext.drawInfo;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.InputDisplay]))
            {
                MainManager_Ext.showInputDisplay = !MainManager_Ext.showInputDisplay;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.Heal]))
            {
                MainManager.Heal();
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.InfJump]))
            {
                MainManager_Ext.toggleInfJump = !MainManager_Ext.toggleInfJump;
                MainManager.PlaySound("Scroll", -1);
                guiInfoMessage = "Inf. Jump : " + (MainManager_Ext.toggleInfJump ? "On" : "Off");
                guiInfoCount = 1;
            }

            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.ActionTime]))
            {
                MainManager_Ext.toggleActionTime = !MainManager_Ext.toggleActionTime;
                MainManager.PlaySound("Scroll", -1);
                guiInfoMessage = "Action Timer : " + (MainManager_Ext.toggleActionTime ? "On" : "Off");
                guiInfoCount = 1;
            }

            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.Speed]))
            {
                __instance.basespeed = ((__instance.basespeed == 5) ? 10 : 5);
                speed = __instance.basespeed;
                guiInfoMessage = "Speed : " + __instance.basespeed;
                guiInfoCount = 1;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.Save]) && !MainManager.instance.pause)
            {
                MainManager.Save(new Vector3?(MainManager.player.transform.position));
                MainManager.PlaySound("Save", -1);
                guiInfoMessage = "Game saved .";
                guiInfoCount = 1;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.Reload]) && !MainManager.instance.pause)
            {
                MainManager.ReloadSave();
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.MainMenu]))
            {
                MainManager.Reset();
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.DownPos]))
            {
                if (MainManager_Ext.pp_TeleportIndex == 0)
                {
                    MainManager_Ext.pp_TeleportIndex = 4;
                }
                else
                {
                    MainManager_Ext.pp_TeleportIndex--;
                }
                guiInfoMessage = "Pos " + (MainManager_Ext.pp_TeleportIndex + 1) + " selected .";
                guiInfoCount = 1;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.UpPos]))
            {
                if (MainManager_Ext.pp_TeleportIndex == 4)
                {
                    MainManager_Ext.pp_TeleportIndex = 0;
                }
                else
                {
                    MainManager_Ext.pp_TeleportIndex++;
                }
                guiInfoMessage = "Pos " + (MainManager_Ext.pp_TeleportIndex + 1) + " selected .";
                guiInfoCount = 1;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.SavePos]))
            {
                MainManager_Ext.pp_TeleportArray[MainManager_Ext.pp_TeleportIndex] = MainManager.player.transform.position;
                MainManager.PlaySound("Confirm", -1);
                guiInfoMessage = "Pos " + (MainManager_Ext.pp_TeleportIndex + 1) + " saved .";
                guiInfoCount = 1;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.LoadPos]))
            {
                MainManager.player.transform.position = MainManager_Ext.pp_TeleportArray[MainManager_Ext.pp_TeleportIndex];
                MainManager.PlaySound("Confirm", -1);
                guiInfoMessage = "Pos " + (MainManager_Ext.pp_TeleportIndex + 1) + " loaded .";
                guiInfoCount = 1;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.ToggleCollision]))
            {
                MainManager_Ext.toggleCollision = !MainManager_Ext.toggleCollision;
                __instance.entity.detect.enabled = !MainManager_Ext.toggleCollision;
                __instance.entity.ccol.enabled = !MainManager_Ext.toggleCollision;
                __instance.entity.feet.enabled = !MainManager_Ext.toggleCollision;
                MainManager_Ext.toggleInfJump = MainManager_Ext.toggleCollision;
                if (MainManager_Ext.toggleCollision)
                    __instance.entity.hitwall = false;
                MainManager.PlaySound("Scroll", -1);
                guiInfoMessage = "Collision : " + (MainManager_Ext.toggleCollision ? "Off" : "On");
                guiInfoCount = 1;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.FreeCam]))
            {
                MainManager_Ext.toggleFreeCam = !MainManager_Ext.toggleFreeCam;
                MainManager.PlaySound("Scroll", -1);
                guiInfoMessage = "FreeCam : " + (MainManager_Ext.toggleFreeCam ? "On" : "Off");
                guiInfoCount = 1;

                if (MainManager_Ext.toggleFreeCam)
                    Cursor.lockState = CursorLockMode.Confined;
                else
                    Cursor.lockState = CursorLockMode.None;
            }
        }

        public void ILPracticeKeys()
        {
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.ResetIL]) &&!MainManager.instance.pause && MainManager_Ext.ilMode)
            {
                MainManager.instance.GetComponent<ILTimer>().ResetIL();
            }

            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.PerfectRNG]))
            {
                MainManager_Ext.togglePerfectRNG = !MainManager_Ext.togglePerfectRNG;
                MainManager.PlaySound("Scroll", -1);
                guiInfoMessage = "Perfect RNG : " + (MainManager_Ext.togglePerfectRNG ? "On" : "Off");
                guiInfoCount = 1;
            }

            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.ToggleGhost]))
            {
                var ilTimer = MainManager.instance.GetComponent<ILTimer>();

                if (ilTimer.ghost != null)
                {
                    ilTimer.ghost.gameObject.SetActive(ILTimer.hideGhost);
                }
                ILTimer.hideGhost = !ILTimer.hideGhost;
                MainManager.PlaySound("Scroll", -1);
                guiInfoMessage = "Ghost : " + (ILTimer.hideGhost ? "Off" : "On");
                guiInfoCount = 1;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.UndoSplit]))
            {
                var ilTimer = MainManager.instance.GetComponent<ILTimer>();

                ilTimer.UndoSplit();
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.FreezeResistance]))
            {
                MainManager_Ext.noFreezeRes = !MainManager_Ext.noFreezeRes;
                MainManager.PlaySound("Scroll", -1);
                guiInfoMessage = "Freeze Resistance : " + (!MainManager_Ext.noFreezeRes ? "On" : "Off");
                guiInfoCount = 1;
            }
            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.HideTimer]))
            {
                var ilTimer = MainManager.instance.GetComponent<ILTimer>();
                ILTimer.showTimer = !ILTimer.showTimer;
                ilTimer.timerUI.gameObject.SetActive(ILTimer.showTimer);
                MainManager.PlaySound("Scroll", -1);
            }

            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.TextStorage]))
            {
                MainManager_Ext.toggleTextStorage = !MainManager_Ext.toggleTextStorage;
                if (MainManager_Ext.toggleTextStorage)
                {
                    StartCoroutine(MainManager.SetText("this is empty text for text storage", null, null));
                    MainManager.instance.message = false;
                    MainManager.instance.minipause = false;
                }
                MainManager.PlaySound("Scroll", -1);
            }

            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.AntiSoftlock]))
            {
                MainManager.instance.message = false;
                MainManager.instance.minipause = false;
                MainManager.instance.inevent = false;
                MainManager.instance.pause = false;
                MainManager.PlaySound("Scroll", -1);
            }
        }

        public void GUI_DrawInfoBox(GUIStyle guiStyle)
        {
            if (guiInfoCount <= 0)
            {
                return;
            }
            GUI.Box(new Rect(0f, (float)Screen.height - 340f, 150f, 32f), guiInfoMessage, guiStyle);
            guiInfoCount++;
            if (guiInfoCount > 180)
            {
                guiInfoCount = 0;
                guiInfoMessage = "";
            }
        }

        public void GUI_DrawInputDisplay(GUIStyle guiStyle)
        {
            if (!MainManager_Ext.showInputDisplay)
            {
                return;
            }
            guiStyle.fontSize = 22;
            guiStyle.fontStyle = FontStyle.Bold;
            float num = (float)Screen.height - 42f;
            GUI.Box(new Rect(0f, num, 220f, 42f), "", guiStyle);
            guiStyle.normal.background = null;
            if (MainManager.GetKey(2, true))
            {
                GUI.Box(new Rect(0f, num, 32f, 42f), "←", guiStyle);
            }
            if (MainManager.GetKey(0, true))
            {
                GUI.Box(new Rect(26f, num, 32f, 42f), "↑", guiStyle);
            }
            if (MainManager.GetKey(1, true))
            {
                GUI.Box(new Rect(42f, num, 32f, 42f), "↓", guiStyle);
            }
            if (MainManager.GetKey(3, true))
            {
                GUI.Box(new Rect(58f, num, 32f, 42f), "→", guiStyle);
            }
            if (MainManager.GetKey(4, true))
            {
                GUI.Box(new Rect(106f, num, 32f, 42f), "J", guiStyle);
            }
            if (MainManager.GetKey(5, true))
            {
                GUI.Box(new Rect(128f, num, 32f, 42f), "A", guiStyle);
            }
            if (MainManager.GetKey(6, true))
            {
                GUI.Box(new Rect(154f, num, 32f, 42f), "S", guiStyle);
            }
            if (MainManager.GetKey(9, true))
            {
                GUI.Box(new Rect(178f, num, 32f, 42f), "En", guiStyle);
            }
        }

        public void GUI_DrawPMAInfo(PlayerControl __instance, GUIStyle guiStyle)
        {
            if (!MainManager_Ext.drawInfo)
            {
                return;
            }
            var beemerang = GameObject.Find("Beerang(Clone)");

            float flyHeight = transform.position.y - startHeight;
            GUI.Box(new Rect(0f, Screen.height - 350f, 300f, 300f),
              $"Beemerang Pos : {(beemerang != null ? beemerang.transform.position.ToString("F4") : "")}\n" +
              $"Axis 1 : {Input.GetAxis("1")}\n" +
              $"Axis 2 : {Input.GetAxis("2")}\n" +
              $"Area ID : {MainManager.map.areaid}\n" +
              $"Map : {MainManager.map.mapid}\n"+ 
              $"Map ID : {MainManager.map.name}\n" +
              $"EventID : {(MainManager.instance.inevent ? MainManager.lastevent.ToString() : "not in event")}\n\n" +
              $"Position : {MainManager.player.transform.position.ToString("F4")}\n" +
              $"Angle: {Quaternion.Angle(MainManager.player.transform.rotation,MainManager.player.entity.detect.transform.rotation)}\n" +
              $"Speed: {__instance.entity.rigid.velocity.ToString("F4")}\n"+
              $"Fly Height: {(MainManager.player.flying ? flyHeight.ToString() : "not flying")}\n"+
              $"Fly Jump Frame: {MainManager_Ext.flyHoldFrames}\n"+
              $"Last Respawn: {__instance.lastpos}\n"
              , guiStyle);
        }

        public void OnGUI()
        {
            var playerControl = this.gameObject.GetComponent<PlayerControl>();
            GUIStyle guistyle = new GUIStyle();
            guistyle.fontSize = 16;
            guistyle.font = MainManager.fonts[0];
            guistyle.normal.textColor = Color.white;
            guistyle.padding = new RectOffset(8, 0, 8, 0);
            Texture2D texture2D = new Texture2D(1, 1);
            texture2D.SetPixel(0, 0, new Color(0.1f, 0.1f, 0.1f, 0.45f));
            texture2D.Apply();
            guistyle.normal.background = texture2D;
            GUI_DrawInfoBox(guistyle);
            GUI_DrawPMAInfo(playerControl, guistyle);
            GUI_DrawInputDisplay(guistyle);
        }
    }
}
