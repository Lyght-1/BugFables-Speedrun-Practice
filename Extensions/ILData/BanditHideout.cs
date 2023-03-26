using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Extensions.ILData
{
    public class BanditHideout : SplitGroup
    {
        public BanditHideout(List<Split> splits) : base(splits){}

        public override void SetupData()
        {
            ilType = IL.BanditHideout;
            startMap = MainManager.Maps.DesertBadlands;
            startPos = new Vector3(18.8f, 2, -8.5f);
            items = new List<int> { 70, 70, (int)MainManager.Items.ShockShroom };
            keyItems = new List<int>{27,41, (int)MainManager.Items.GHCrank,105 };

            crystalBerryAmount = 0;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,18,19,34,28,42,53,22,36,40 },
                new List<int>(){ 14,1,2,4,33,49,56,26,38,50,7,3,30 },
                new List<int>(){ 11,12,13,6 }
            };

            flags = new List<int>
            {
                1,3,4,7,8,10,11,15,16,17,20,21,22,23,24,27,30,31,32,41,44,47,50,56,66,67,68,73,74,75,76,84,85,86,88,90,92,93,94,
                95,96,97,98,100,101,102,103,104,107,108,109,111,113,114,115,116,117,118,119,120,122,124,125,126,127,128,129,130,
                133,134,135,138,140,141,159,160,167,168,169,170,172,173,174,176,177,178,179,182,201,202,206,211,212,213,214,215,
                216,217,218,221,222,224,239,240,250,258,278,281,282,299,300,302,307,415,479,482,621,579,617,618,621,622,660,663,
                690,691,694,697,699
            };

            cbFlags = new List<int>{0,2,4,5,8,9,12,15,16,18};

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,0},
                new int[2]{24,0},
                new int[2]{6,0},
                new int[2]{15,-1},
                new int[2]{14,-2},
                new int[2]{23,-2},
                new int[2]{49,-2},
            };

            money = 28;
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

        public override void DoSpecifics(){}

        public override void SetSplits()
        {
            splits = new List<Split>
            {
                new Split("Enter Hideout", MainManager.Maps.HideoutEntrance, TypeSplit.Room),
                new Split("Cell Room", MainManager.Maps.HideoutCell, TypeSplit.Room),
                new Split("Central Room", MainManager.Maps.HideoutCentralRoom, TypeSplit.Room),
                new Split("Ice Puzzle", MainManager.Maps.HideoutLeftA, TypeSplit.Room),
                new Split("Garden", MainManager.Maps.HideoutGarden, TypeSplit.Room,endID: MainManager.Maps.HideoutWestStorage)
            };
        }
    }
}
