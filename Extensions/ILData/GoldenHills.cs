using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Extensions.ILData
{
    public class GoldenHills : SplitGroup
    {
        public GoldenHills(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            offset = TimeSpan.Parse("00:00:25.000");
            ilType = IL.GoldenHills;
            startMap = MainManager.Maps.GoldenSettlement1;
            startPos = new Vector3(0, 0, 28);
            items = new List<int> { 2, 2, 172, 172, 62 };
            keyItems = new List<int> { 27, 41, 174, 56, 55 };
            crystalBerryAmount = 6;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,38,50 },
                new List<int>(){ 12,1,2,4,33,49,56},
                new List<int>(){ 11,0 }
            };

            flags = new List<int>
            {
                1,10,11,15,16,17,21,24,27,30,31,32,41,67,68,73,74,84,85,86,90,95,96,97,98,103,108,119,120,691,694,
            };

            cbFlags = new List<int>{ 0,1,2,4,5,8 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2},
                new int[2]{24,-2},
            };

            money = 0;
            tp = 10;
            maxTp = 10;
            bp = 5;
            maxbp = 5;
            level = 1;
            exp = 77;
            maxExp = 100;
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
                new Split("Enter Golden Hills", MainManager.Maps.GoldenHillsDungeonEntrance, TypeSplit.Room),
                new Split("Left Big Room", MainManager.Maps.GoldenHillsDungeonLeftMain, TypeSplit.Room),
                new Split("Magic Seed Room", MainManager.Maps.GoldenHillsDungeonCrankLeft, TypeSplit.Room),
                new Split("Left Big Room 2", MainManager.Maps.GoldenHillsDungeonLeftMain, TypeSplit.Room),
                new Split("Venus Bud Room", MainManager.Maps.GoldenHillsDungeonLeftCrankHalf, TypeSplit.Room),
                new Split("Left Big Room 3", MainManager.Maps.GoldenHillsDungeonLeftMain, TypeSplit.Room),
                new Split("Main Room 2", MainManager.Maps.GoldenHillsDungeonEntrance, TypeSplit.Room),
                new Split("Chomper Room", MainManager.Maps.GoldenHillsLowerRightCrank, TypeSplit.Room),
                new Split("Main Room 3", MainManager.Maps.GoldenHillsDungeonEntrance, TypeSplit.Room),
                new Split("Leif Strat Room", MainManager.Maps.GoldenHillsDungeonRightCrank, TypeSplit.Room, MainManager.Maps.GoldenHillsDungeonEntrance)
            };
        }
    }
}
