using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class Watcher : SplitGroup
    {
        public Watcher(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            ilType = IL.Watcher;
            startMap = MainManager.Maps.SandCastleRoof;
            startPos = new Vector3(-0.11f, 0, -1.8f);
            items = new List<int> { 70 };
            keyItems = new List<int> { 27, 41, (int)MainManager.Items.GHCrank };
            crystalBerryAmount = 1;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,18,19,34,28,42,53,22,36,40 },
                new List<int>(){ 14,1,2,4,33,49,56,26,38,50,7,3,30 },
                new List<int>(){ 11,12,13,6 }
            };

            flags = new List<int>
            {
                1,10,11,13,14,15,16,17,18,20,21,22,23,24,27,28,29,30,31,32,33,34,35,36,41,44,47,50,56,66,67,68,73,74,75,76,84,85,86,88,90,92,
                93,94,95,96,97,98,100,101,102,103,107,108,109,111,113,114,115,116,117,118,119,120,122,124,125,126,127,128,129,130,133,134,135,
                138,140,141,159,160,167,168,169,170,172,173,174,176,177,178,179,182,201,202,206,211,212,213,214,215,216,217,218,221,222,239,240,
                250,258,259,269,278,280,281,282,284,288,289,292,293,294,295,296,297,298,299,300,301,302,303,306,307,415,479,482,621,579,617,660,
                663,690,691,694,697,699
            };

            cbFlags = new List<int> { 0,1, 2, 4, 5, 8, 9, 12, 15, 16, 18 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2},
                new int[2]{24,0},
                new int[2]{6,0},
                new int[2]{49,0},
                new int[2]{15,-2},
                new int[2]{14,-2},
                new int[2]{23,1}
            };

            money = 24;
            tp = 16;
            maxTp = 16;
            bp = 3;
            maxbp = 14;
            level = 6;
            exp = 16;
            maxExp = 105;
            inventorySpace = 10;

            discoveries = new List<int> { 0, 1, 3, 6, 7, 11, 12, 13,16, 20, 21 };
            seenAreas = null;
            shadesPool = null;
        }

        public override void DoSpecifics()
        {
            MainManager.Heal(true, true);
        }

        public override void SetSplits()
        {
            var watcher = new MainManager.Enemies[] { MainManager.Enemies.ZombieRoach };
            splits = new List<Split>
            {
                new Split("Watcher", MainManager.Maps.SandCastleBossRoom, TypeSplit.BattleEnd,enemies:watcher, endID:MainManager.Maps.SandCastleBossRoom),
            };
        }
    }
}
