using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Extensions.ILData
{
    public class UpperGolden : SplitGroup
    {
        public UpperGolden(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            offset = TimeSpan.Parse("00:00:22.080");
            ilType = IL.UpperGolden;
            startMap = MainManager.Maps.GoldenHillsDungeonEntrance;
            startPos = new Vector3(-0.16f, 0.39f, 19.8f);
            items = new List<int> { 2, 2, 172 };
            keyItems = new List<int> { 27, 41, 174, 56, 55 };
            crystalBerryAmount = 7;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,38,50 },
                new List<int>(){ 12,1,2,4,33,49,56},
                new List<int>(){ 11,0 }
            };

            flags = new List<int>
            {
                1,10,11,15,16,17,21,24,27,30,31,32,41,56,67,68,73,74,84,85,86,90,95,96,97,98,103,108,109,110,111,112,113,116,117,118,
                119,120,122,663,691,694
            };

            cbFlags = new List<int> { 0, 2, 4, 5, 8, 9 };

            medals = new List<int[]>
            {
                new int[2]{11,-1},
                new int[2]{0, 1},
                new int[2]{24,-2},
            };

            money = 13;
            tp = 0;
            maxTp = 13;
            bp = 3;
            maxbp = 5;
            level = 2;
            exp = 88;
            maxExp = 101;
            inventorySpace = 10;
            discoveries = null;
            seenAreas = null;
            shadesPool = null;
        }

        public override void DoSpecifics()
        {
        }

        public override void SetSplits()
        {
            splits = new List<Split>
            {
                new Split("Sun Stone", MainManager.Maps.GoldenHillsDungeonUpperMain, TypeSplit.Room),
                new Split("Respawn", MainManager.Maps.GoldenHillsDungeonUpperSide, TypeSplit.Room),
                new Split("Moon Stone", MainManager.Maps.GoldenHillsDungeonUpperMain, TypeSplit.Room,  MainManager.Maps.GoldenHillsDungeonBoss)
            };
        }
    }
}
