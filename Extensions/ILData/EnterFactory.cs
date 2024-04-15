using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Extensions.ILData
{
    public class EnterFactory : SplitGroup
    {
        public EnterFactory(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            offset = TimeSpan.Parse("00:00:15.100");
            ilType = IL.EnterFactory;
            startMap = MainManager.Maps.DesertCaravanMap;
            startPos = new Vector3(-33.6f, 0, 1.8f);
            items = new List<int> { 17, 17, 26, 1, 2, 2 };
            keyItems = new List<int> { 27, 41, (int)MainManager.Items.GHCrank };
            crystalBerryAmount = 4;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23 },
                new List<int>(){ 13,1,2,3,4,7,33,38,49,50,56 },
                new List<int>(){ 11,12,6 }
            };

            flags = new List<int>
            {
                1,2,3,7,8,10,11,13,14,15,16,17,21,22,23,24,27,28,29,30,31,32,33,34,35,36,41,44,47,50,64,66,67,68,73,74,76,
                84,85,86,88,90,92,93,94,95,96,97,98,100,101,102,103,104,107,108,109,110,111,112,113,114,115,116,117,118,119,
                120,122,124,125,126,127,128,129,130,133,134,135,138,140,141,174,182,201,202,206,209,240,278,287,307,479,482,
                579,617,618,621,663,691,694,699
            };

            cbFlags = new List<int> { 0,1, 2, 4, 5, 8, 9, 12,15 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2},
                new int[2]{24,2},
                new int[2]{6,2},
                new int[2]{15,-2},
                new int[2]{14,-2}
            };

            money = 16;
            tp = 0;
            maxTp = 16;
            bp = 1;
            maxbp = 5;
            level = 3;
            exp = 81;
            maxExp = 102;
            inventorySpace = 10;
            discoveries = new List<int> { 0, 1, 6, 7, 11, 12, 13, 20, 21, 30 };
            seenAreas = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
            shadesPool = new List<int> { 9, 42, 19, 43 };
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
            splits = new List<Split>
            {
                new Split("Reach DR", MainManager.Maps.DesertBadgeAlcove, TypeSplit.Room),
                new Split("Museum", MainManager.Maps.DefiantRoot1, TypeSplit.Room),
                new Split("Elevator", MainManager.Maps.DefiantRoot2, TypeSplit.Room),
                new Split("Get BS", MainManager.Maps.BeehiveScannerRoom, TypeSplit.Room),
                new Split("Balcony", MainManager.Maps.BeehiveBalcony, TypeSplit.Room),
                new Split("Main Area 1", MainManager.Maps.BeehiveMainArea, TypeSplit.Room),
                new Split("Queen", MainManager.Maps.BeehiveThroneRoom, TypeSplit.Room),
                new Split("Main area 2", MainManager.Maps.BeehiveMainArea, TypeSplit.Room),
                new Split("Outside", MainManager.Maps.BeehiveOutside, TypeSplit.Room, endID:MainManager.Maps.HoneyFactoryEntrance)
            };
        }
    }
}
