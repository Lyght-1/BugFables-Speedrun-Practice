using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class HoneyFactory : SplitGroup
    {
        public HoneyFactory(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            ilType = IL.HoneyFactory;
            startMap = MainManager.Maps.BeehiveOutside;
            startPos = new Vector3(-63, 0, 35);
            items = new List<int>{17,17,26,2,2,1};
            keyItems = new List<int> { 27, 41 };
            crystalBerryAmount = 3;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,18,19,38,34,0 },
                new List<int>(){ 13,1,2,4,33,49,56,26,38,50,7,3},
                new List<int>(){ 11,12,6 }
            };

            flags = new List<int>
            {
                1,10,11,15,16,17,21,24,27,30,31,32,41,56,67,68,73,74,76,84,85,86,88,90,95,96,97,98,103,108,119,120,122,
                124,125,126,130,133,138,159,160,167,168,169,170,172,174,201,202,206,621,579,617,660,663,691,694,699
            };

            cbFlags = new List<int>{ 0,2,4,5,8,9,13,15 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2},
                new int[2]{24,2},
                new int[2]{6,2},
                new int[2]{15,-2},
                new int[2]{14,-2}
            };

            money = 20;
            tp = 16;
            maxTp = 16;
            bp = 5;
            maxbp = 5;
            level = 3;
            exp = 81;
            maxExp = 102;
            inventorySpace = 10;
            discoveries = null;
            seenAreas = null;
            shadesPool = null;
        }

        public override void DoSpecifics()
        {
            MainManager.instance.flagvar[24] = 0;
        }

        public override void SetSplits()
        {
            var enemies = new MainManager.Enemies[] { MainManager.Enemies.BeeBot, MainManager.Enemies.BeeBot };

            splits = new List<Split>
            {
                new Split("Enter Factory", MainManager.Maps.HoneyFactoryEntrance, TypeSplit.Room),
                new Split("OverseerRoom", MainManager.Maps.HoneyFactoryWorkerRooms, TypeSplit.Room),
                new Split("Main Room 2", MainManager.Maps.HoneyFactoryEntrance, TypeSplit.Room),
                new Split("BubbleShield", MainManager.Maps.FactoryProcessingFirstRoom, TypeSplit.Room),
                new Split("FactoryProcess2", MainManager.Maps.FactoryProcessing2, TypeSplit.Room),
                new Split("Big Room", MainManager.Maps.FactoryProcessingPump, TypeSplit.Room),
                new Split("Top puzzle", MainManager.Maps.FactoryProcessingPuzzle2, TypeSplit.Room),
                new Split("Big Room 2", MainManager.Maps.FactoryProcessingPump, TypeSplit.Room),
                new Split("Cursed Room", MainManager.Maps.FactoryProcessingPuzzle3, TypeSplit.Room),
                new Split("Big Room 3", MainManager.Maps.FactoryProcessingPump, TypeSplit.Room),
                new Split("Beep boops", MainManager.Maps.FactoryProcessingPuzzle1, TypeSplit.BattleEnd, enemies: enemies, endID: MainManager.Maps.FactoryProcessingPuzzle1)
            };
        }
    }
}
