using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Extensions.ILData
{
    public class MerchantsRescue : SplitGroup
    {
        public MerchantsRescue(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            offset = TimeSpan.Parse("00:00:32.580");
            ilType = IL.MerchantsRescue;
            startMap = MainManager.Maps.GoldenPathTunnel;
            startPos = new Vector3(0.38f, 0, -8);
            items = new List<int> {17,17,17,26,1,2,2 };
            keyItems = new List<int> { 27, 41};
            crystalBerryAmount = 7;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23 },
                new List<int>(){ 13,1,2,3,4,7,33,38,49,50,56 },
                new List<int>(){ 11,12,6 }
            };

            flags = new List<int>
            {
                1,2,3,7,10,11,13,14,15,16,17,21,22,23,24,27,28,29,30,31,32,33,34,35,36,41,44,47,50,64,66,67,68,73,74,76,
                84,85,86,88,90,93,94,95,96,97,98,100,101,102,103,104,107,108,109,110,111,112,113,114,115,116,117,118,119,
                120,122,124,125,126,127,128,129,130,133,134,135,174,182,287,307,240,479,482,579,617,618,621,663,691,694
            };

            cbFlags = new List<int> { 0, 2, 4, 5, 8, 9,12 };

            medals = new List<int[]>
            {
                new int[2]{11,-1},
                new int[2]{0, 0},
                new int[2]{24,-2},
                new int[2]{14,-2},
                new int[2]{15,-2},
            };

            money = 24;
            tp = 0;
            maxTp = 16;
            bp = 3;
            maxbp = 5;
            level = 3;
            exp = 36;
            maxExp = 102;
            inventorySpace = 10;
            discoveries = new List<int> { 0, 1, 6, 7, 11, 12, 13, 21 };
            seenAreas = new List<int> { 0, 1, 2, 4, 5, 6 };
            shadesPool = new List<int> { 9, 6, 42, 19, 43 };
        }

        public override void DoSpecifics()
        {
            MainManager.instance.flagvar[19] = 3;
            MainManager.instance.flagvar[18] = 3;
            MainManager.instance.flagvar[53] = 1;
            MainManager.instance.flagvar[54] = 5;
            MainManager.instance.flagvar[58] = 1;
            MainManager.instance.flagvar[55] = 3;
        }

        public override void SetSplits()
        {
            var enemies = new MainManager.Enemies[] { MainManager.Enemies.Burglar, MainManager.Enemies.Thief, MainManager.Enemies.WaspHealer };
            splits = new List<Split>
            {
                new Split("Reach Plaza", MainManager.Maps.BOGoldenPath, TypeSplit.Room),
                new Split("Reach Tavern", MainManager.Maps.BugariaMainPlaza, TypeSplit.Room),
                new Split("Leave City", MainManager.Maps.UndergroundBar, TypeSplit.Room),
                new Split("Outskirts", MainManager.Maps.BugariaOutskirtsEast1, TypeSplit.Room),
                new Split("Lost Sands", MainManager.Maps.BOLostSandsEntrance, TypeSplit.Room),
                new Split("Bandits", MainManager.Maps.DesertCaravanMap, TypeSplit.BattleEnd, enemies:enemies, endID:MainManager.Maps.DesertCaravanMap)
            };
        }
    }
}
