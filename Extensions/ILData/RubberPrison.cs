using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class RubberPrison : SplitGroup
    {
        public RubberPrison(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            ilType = IL.RubberPrison;
            startMap = MainManager.Maps.MetalLake;
            startPos = new Vector3(14f, -0.37f, 15.59f);
            items = new List<int>{ 130,130,130,130,130,172,172 };
            keyItems = new List<int>{27,41, (int)MainManager.Items.GHCrank,116 };
            crystalBerryAmount = 0;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,18,19,34,28,42,53,22,36,40,25,5,35,58,57,37,43,44,47,48,59,45,0 },
                new List<int>(){ 16,1,2,4,33,49,56,26,38,50,7,3,30 },
                new List<int>(){ 11,12,13,14,15,6 }
            };

            flags = new List<int>
            {
                1,10,11,13,14,15,16,17,18,19,20,21,22,23,24,27,30,31,32,33,34,35,36,38,39,41,56,66,67,68,73,74,75,76,77,80,84,85,86,88,90,95,96,97,98,103,108,119,120,122,
                124,125,126,130,133,138,140,141,159,160,167,168,169,170,172,173,174,176,177,178,179,201,202,206,211,212,213,214,215,216,217
                ,218,221,222,224,239,250,258,259,260,269,278,280,281,282,284,288,289,290,291,292,293,294,295,296,297,298,299,300,301,302,303
                ,304,306,307,322,323,334,335,336,337,345,347,348,350,357,359,360,363,364,365,366,367,368,369,370,371,372,374,376,377,379,383,384,385,386,409,415,416,420,
                447,448,479,482,606,617,621,622,642,579,617,657,660,663,691,694,699,697,708,
            };

            cbFlags = new List<int>{0,2,4,5,8,9,12,13,15,16,18,29,30,43,44};

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
                new int[2]{23,1}
            };

            money = 100;
            tp = 19;
            maxTp = 19;
            bp = 1;
            maxbp = 17;
            level = 8;
            exp = 31;
            maxExp = 107;
            inventorySpace = 10;

            discoveries = new List<int>{0,1,3,6,7,11,12,13,16,20,21,25};
            seenAreas = new List<int>{0,1,2,3,4,5,6,7,8,9,10,11,12,13,16,18,19,20};
            shadesPool = null;
        }

        public override void DoSpecifics()
        {
        }

        public override void SetSplits()
        {
            splits = new List<Split>
            {
                new Split("Enter Prison", MainManager.Maps.RubberPrisonPier, TypeSplit.Room),
                new Split("Checkout", MainManager.Maps.RubberPrisonCheckpointCorridor, TypeSplit.Room),
                new Split("Spike Room", MainManager.Maps.RubberPrisonSpikeRoom, TypeSplit.Room),
                new Split("Left Side Skip", MainManager.Maps.RubberPrisonCells1, TypeSplit.Room),
                new Split("Respawn Strat", MainManager.Maps.RubberPrisonCells2, TypeSplit.Room),
                new Split("Third Floor", MainManager.Maps.RubberPrisonThirdFloor, TypeSplit.Room),
                new Split("Outside", MainManager.Maps.RubberPrisonPier, TypeSplit.Room),
                new Split("Crank", MainManager.Maps.RubberPrisonOffice, TypeSplit.Room),
                new Split("Outside 2", MainManager.Maps.RubberPrisonPier, TypeSplit.Room,  endID: MainManager.Maps.RubberPrisonGiantLairBridge)
            };
        }
    }
}
