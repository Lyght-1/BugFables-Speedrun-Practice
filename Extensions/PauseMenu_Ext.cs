using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace SpeedrunPractice.Extensions
{
    class PauseMenu_Ext : MonoBehaviour
    {
        public static bool checkingPBs = false;
        public static int maxOption = 0;
        public static ButtonSprite pButton = null;
        public static void BuildPBWindow(List<string> list)
        {
            if (checkingPBs)
            {
                list.Clear();
                var ilTimer = MainManager.instance.GetComponent<ILTimer>();
                var counter = 0;
                foreach(var splitGroup in ilTimer.splitGroups)
                {
                    if(splitGroup != null)
                    {
                        TimeSpan sumo = TimeSpan.Zero;
                        var splitList = new List<string>();
                        var ilName = (IL)counter;
                        var size = "0.5,0.5";
                        if(splitGroup.splits.Count > 13)
                        {
                            size = "0.45,0.35";
                        }

                        splitList.Add("|size,"+size+"||font,0|-----------------------"+ splitGroup.name + "-----------------------|line||font,3|");
                        splitList.Add("---Split Name----------PB-----------Segment---------Gold--");
                        foreach (var split in splitGroup.splits)
                        {
                            sumo = sumo.Add(split.goldTime);
                            string pbTime = Split.GetTimeFormat(split.pbTime);
                            string segmentTime = Split.GetTimeFormat(split.segmentTime);
                            string goldTime = Split.GetTimeFormat(split.goldTime);
                            string splitName = split.name.Length > 17 ? split.name.Substring(0, 12) + "..." : split.name;
                            splitList.Add(splitName.PadRight(15, '.') + "-----" + pbTime + "------" + segmentTime + "------" + goldTime);         
                        }
                        string sumOfBest = Split.GetTimeFormat(sumo);
                        splitList[splitList.Count - 1] += "|line||font,2|";
                        splitList.Add("|size,0.5,0.5|Sum of Best : " + sumOfBest + " ------- Attempts : " + splitGroup.attemptsCount);
                        list.Add(string.Join("|line|", splitList.ToArray()));
                    }
                    counter++;
                }
                maxOption = list.Count;
                var maxsecondRef = AccessTools.FieldRefAccess<PauseMenu,int>("maxsecond");
                maxsecondRef(MainManager.pausemenu) = list.Count;
            }
        }
    }
}
