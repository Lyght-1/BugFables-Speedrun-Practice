using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class Abomihoneys : SplitGroup
    {
        public Abomihoneys(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            startSplit = TypeSplit.BattleEnd;
            ilType = IL.Abomihoneys;
            startMap = MainManager.Maps.FactoryProcessingPuzzle1;
            startPos = new Vector3(-0.76f, 0, 13.21f);
            items = new List<int> { 17, 17, 26, 2, 2, 1 };
            keyItems = new List<int> { 27, 41, 95, 95 };
            crystalBerryAmount = 4;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,18,19,38,34,0 },
                new List<int>(){ 13,1,2,4,33,49,56,26,38,50,7,3},
                new List<int>(){ 11,12,6 }
            };

            flags = new List<int>
            {
                1,2,3,7,8,10,11,13,14,15,16,17,20,21,22,23,24,27,28,29,30,31,32,33,34,35,36,41,44,47,50,64,66,67,68,73,74,76,
                84,85,86,88,90,92,93,94,95,96,97,98,100,101,102,103,104,107,108,109,110,111,112,113,114,115,116,117,118,119,
                120,122,124,125,126,127,128,129,130,133,134,135,138,140,141,159,160,167,168,169,170,172,174,177,178,179,182,201,202,206,209,
                212,215,240,278,287,307,479,482,579,617,618,621,660,663,691,694,699
            };

            cbFlags = new List<int> { 0, 2, 4, 5, 8, 9, 13, 15,16 };

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
            tp = 16;
            maxTp = 16;
            bp = 1;
            maxbp = 5;
            level = 3;
            exp = 81;
            maxExp = 102;
            inventorySpace = 10;
            discoveries = new List<int> { 0, 1, 6, 7, 11, 12, 13, 20, 21,24, 29 };
            seenAreas = new List<int> { 0, 1, 2, 3, 4, 5, 6,10,12,13 };
            shadesPool = new List<int> { 9, 42, 19, 43 };
        }

        public override void DoSpecifics()
        {
            MainManager.instance.flagvar[24] = 0;
            MainManager.instance.flagvar[19] = 3;
            MainManager.instance.flagvar[18] = 3;
            MainManager.instance.flagvar[53] = 1;
            MainManager.instance.flagvar[54] = 5;
            MainManager.instance.flagvar[58] = 1;
            MainManager.instance.flagvar[55] = 3;
        }

        public override void SetSplits()
        {
            var abomihoneys = new MainManager.Enemies[] { MainManager.Enemies.Abomihoney, MainManager.Enemies.Abomihoney, MainManager.Enemies.Abomihoney };
            var beepboops = new MainManager.Enemies[] { MainManager.Enemies.BeeBot, MainManager.Enemies.BeeBot };
            splits = new List<Split>
            {
                new Split("Moving Platforms", MainManager.Maps.FactoryProcessingPuzzle1, TypeSplit.Room, enemies:beepboops),
                new Split("Main Room", MainManager.Maps.FactoryProcessingPump, TypeSplit.Room),
                new Split("Abomihoneys", MainManager.Maps.FactoryProcessingMalbee, TypeSplit.BattleEnd, enemies: abomihoneys, endID: MainManager.Maps.FactoryProcessingMalbee)
            };
        }
    }
}
