using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class EnterFG : SplitGroup
    {
        public EnterFG(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            offset = TimeSpan.Parse("00:00:59.300");
            ilType = IL.EnterFG;
            startMap = MainManager.Maps.SandCastleTreasureRoom;
            startPos = new Vector3(-8, 0, 0);
            items = new List<int> { };
            keyItems = new List<int>{27,41, (int)MainManager.Items.GHCrank, 174,116};
            crystalBerryAmount = 1;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,18,19,34,28,42,53,22,36,40,25,0 },
                new List<int>(){ 1,2,4,33,49,56,26,38,50,7,3,30 },
                new List<int>(){ 11,12,13,14,6 }
            };

            flags = new List<int>
            {
                1,3,4,7,8,10,11,13,14,15,16,17,18,20,21,22,23,24,27,28,29,30,31,32,33,34,35,36,38,41,44,47,50,66,67,68,73,74,75,76,
                84,85,86,88,90,92,93,94,95,96,97,98,100,101,102,103,104,107,108,109,111,113,114,115,116,117,118,119,120,122,124,
                125,126,127,128,129,130,133,134,135,138,140,141,159,160,167,168,169,170,172,173,174,176,177,178,179,182,201,202,
                206,211,212,213,214,215,216,217,218,221,222,224,239,240,250,258,259,260,269,278,280,281,282,284,288,289,290,292,
                293,294,295,296,297,298,299,300,301,302,303,306,307,345,415,416,479,482,579,617,618,621,622,660,663,690,691,694,
                697,699,708
            };

            cbFlags = new List<int>{0,1,2,4,5,8,9,12,15,16,18};

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2},
                new int[2]{24,-2},
                new int[2]{6,-2},
                new int[2]{15,-2},
                new int[2]{14,-2},
                new int[2]{23,-2},
                new int[2]{49,1},
            };

            money = 33;
            tp = 1;
            maxTp = 16;
            bp = 9;
            maxbp = 14;
            level = 6;
            exp = 41;
            maxExp = 105;
            inventorySpace = 10;

            discoveries = new List<int>{0,1,3,6,7,11,12,13,16,20,21,24,27,29,31};
            seenAreas = null;
            shadesPool = new List<int> { 19, 9, 43, 42, 0, 76 };
        }

        public override void DoSpecifics()
        {
            MainManager_Ext.togglePerfectRNG = true;
            MainManager.instance.flagvar[53] = 1;
            MainManager.instance.flagvar[54] = 5;
            MainManager.instance.flagvar[58] = 1;

            for (int i = 0; i != MainManager.instance.playerdata.Length; i++)
            {
                MainManager.instance.playerdata[i].hp = 1;
            }
        }

        public override void SetSplits()
        {
            var waspKing = new MainManager.Enemies[] { MainManager.Enemies.WaspKingIntermission };
            splits = new List<Split>
            {
                new Split("Leave Castle", MainManager.Maps.SandCastleBossRoom, TypeSplit.Room),
                new Split("Wasp King", MainManager.Maps.DesertSandCastle, TypeSplit.BattleEnd, enemies:waspKing),
                new Split("Disc & Dig CB", MainManager.Maps.AntPalace1, TypeSplit.Room),
                new Split("Theater", MainManager.Maps.BugariaTheater, TypeSplit.Room),
                new Split("Reach Tavern", MainManager.Maps.BugariaMainPlaza, TypeSplit.Room),
                new Split("Reach DR", MainManager.Maps.UndergroundBar, TypeSplit.Room),
                new Split("Leave DR", MainManager.Maps.DefiantRoot2, TypeSplit.Room),
                new Split("Enter FG", MainManager.Maps.DesertDREastEntrance, TypeSplit.Room,endID: MainManager.Maps.FarGrasslands1)
            };
        }
    }
}
