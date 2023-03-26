using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class Beast : SplitGroup
    {
        public Beast(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            ilType = IL.Beast;
            startMap = MainManager.Maps.SwamplandsBridge;
            startPos = new Vector3(19, -7, 16.18f);
            items = new List<int> { 70, 70, 70 };
            keyItems = new List<int> { 27, 41, (int)MainManager.Items.GHCrank, 116 };
            crystalBerryAmount = 0;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,18,19,34,28,42,53,22,36,40,25,5,35,0 },
                new List<int>(){ 15,1,2,4,33,49,56,26,38,50,7,3,30 },
                new List<int>(){ 11,12,13,14,6 }
            };

            flags = new List<int>
            {
                1,10,11,15,16,17,18,20,21,22,24,27,30,31,32,38,39,41,56,66,67,68,73,74,75,76,84,85,86,88,90,95,96,97,98,103,108,119,120,122,
                124,125,126,130,133,138,140,141,159,160,167,168,169,170,172,173,174,176,177,178,179,201,202,206,211,212,213,214,215,216,217
                ,218,221,222,224,239,250,258,259,260,269,278,280,281,282,284,288,289,290,291,292,293,294,295,296,297,298,299,300,301,302,303
                ,304,306,307,322,323,334,335,336,337,345,348,354,357,377,383,415,416,479,482,621,622,642,579,617,660,663,691,694,699,697,708,
            };

            cbFlags = new List<int> { 0, 2, 4, 5, 8, 9, 12, 13, 15, 16, 18, 29, 30, 43, 44 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2},
                new int[2]{24,0},
                new int[2]{6,0},
                new int[2]{6,0},
                new int[2]{15,-1},
                new int[2]{14,-2},
                new int[2]{49,0},
                new int[2]{23,-2}
            };

            money = 30;
            tp = 16;
            maxTp = 16;
            bp = 0;
            maxbp = 14;
            level = 6;
            exp = 86;
            maxExp = 105;
            inventorySpace = 10;

            discoveries = null;
            seenAreas = null;
            shadesPool = null;
        }

        public override void DoSpecifics()
        {
            MainManager.Heal(true, true);
            MainManager_Ext.togglePerfectRNG = true;
        }

        public override void SetSplits()
        {
            var beast = new MainManager.Enemies[] { MainManager.Enemies.Centipede };
            splits = new List<Split>
            {
                new Split("Beast", MainManager.Maps.SwamplandsBoss, TypeSplit.BattleEnd,enemies:beast, endID:MainManager.Maps.SwamplandsBoss)
            };
        }
    }
}
