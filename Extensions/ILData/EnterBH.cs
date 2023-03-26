using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Extensions.ILData
{
    public class EnterBH : SplitGroup
    {
        public EnterBH(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            offset = TimeSpan.Parse("00:00:55.500");
            ilType = IL.EnterBH;
            startMap = MainManager.Maps.HoneyFactoryCore;
            startPos = new Vector3(0, 0, -2);
            items = new List<int> { };
            keyItems = new List<int> { 27, 41, (int)MainManager.Items.GHCrank };
            crystalBerryAmount = 4;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,18,19,34,28 },
                new List<int>(){ 1,2,4,33,49,56,26,38,50,7,3 },
                new List<int>(){ 11,12,13,6 }
            };

            flags = new List<int>
            {
                1,3,4,7,8,10,11,15,16,17,20,21,22,23,24,27,30,31,32,41,44,47,50,56,66,67,68,73,74,75,76,84,85,86,88,90,92,93,94,
                95,96,97,98,100,101,102,103,104,107,108,109,111,113,114,115,116,117,118,119,120,122,124,125,126,127,128,129,130,
                133,134,135,138,140,141,159,160,167,168,169,170,172,173,174,176,177,178,179,182,201,202,206,211,212,213,214,215,
                216,217,218,221,222,240,278,299,307,479,482,621,579,617,618,621,660,663,691,694,699
            };

            cbFlags = new List<int>{0,2,4,5,8,9,12,15,16};

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,0},
                new int[2]{24,0},
                new int[2]{6,0},
                new int[2]{15,-1},
                new int[2]{14,-2}
            };

            money = 4;
            tp = 0;
            maxTp = 16;
            bp = 0;
            maxbp = 8;
            level = 4;
            exp = 96;
            maxExp = 103;
            inventorySpace = 10;
            discoveries = null;
            seenAreas = null;
            shadesPool = new List<int> { 42, 43, 19, 9, 49, 0 };
        }

        public override void DoSpecifics()
        {
        }

        public override void SetSplits()
        {
            splits = new List<Split>
            {
                new Split("Leave Hive", MainManager.Maps.HoneyFactoryEntrance, TypeSplit.Room),
                new Split("Reach Throne", MainManager.Maps.DefiantRoot2, TypeSplit.Room),
                new Split("Reach Tavern", MainManager.Maps.AntPalace2, TypeSplit.Room),
                new Split("Reach Crisbee", MainManager.Maps.UndergroundBar, TypeSplit.Room),
                new Split("Leave DR", MainManager.Maps.DefiantRoot3, TypeSplit.Room),
                new Split("Hawk", MainManager.Maps.DesertDRSouthEntrance, TypeSplit.Room),
                new Split("Enter BH", MainManager.Maps.DesertRoachVillage, TypeSplit.Room,endID: MainManager.Maps.HideoutEntrance)
            };
        }
    }
}
