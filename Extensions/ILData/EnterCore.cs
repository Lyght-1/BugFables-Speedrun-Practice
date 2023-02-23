using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class EnterCore : SplitGroup
    {
        public EnterCore(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            startSplit = TypeSplit.BattleEnd;
            ilType = IL.EnterCore;
            startMap = MainManager.Maps.FactoryStorageMiniboss;
            startPos = new Vector3(-24.6f, 4, -0.1f);
            items = new List<int> { 26, 2, 1 };
            keyItems = new List<int> { 27, 41 };
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
                211,212,213,214,215,216,217,240,278,287,307,479,482,579,617,618,621,660,663,691,694,699
            };

            cbFlags = new List<int> { 0, 2, 4, 5, 8, 9, 13, 15, 16 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,0},
                new int[2]{24,0},
                new int[2]{6,0},
                new int[2]{15,-1},
                new int[2]{14,-2}
            };

            money = 8;
            tp = 0;
            maxTp = 16;
            bp = 0;
            maxbp = 8;
            level = 4;
            exp = 54;
            maxExp = 103;
            inventorySpace = 10;
            discoveries = new List<int> { 0, 1, 6, 7, 11, 12, 13, 20, 21, 24, 29 };
            seenAreas = new List<int> { 0, 1, 2, 3, 4, 5, 6, 10, 12, 13 };
            shadesPool = new List<int> { 9, 42, 19, 43 };
        }

        public override void DoSpecifics()
        {
            MainManager.instance.flagvar[24] = 3;
            MainManager.instance.flagvar[19] = 3;
            MainManager.instance.flagvar[18] = 3;
            MainManager.instance.flagvar[53] = 1;
            MainManager.instance.flagvar[54] = 5;
            MainManager.instance.flagvar[58] = 1;
            MainManager.instance.flagvar[55] = 3;
        }

        public override void SetSplits()
        {
            var ahoneynation = new MainManager.Enemies[] { MainManager.Enemies.Ahoneynation };
            splits = new List<Split>
            {
                new Split("Magic Seed", MainManager.Maps.FactoryStorageMiniboss, TypeSplit.Room, enemies:ahoneynation),
                new Split("Storage", MainManager.Maps.FactoryStorageMaze, TypeSplit.Room),
                new Split("Overseer", MainManager.Maps.FactoryStorageOverseer, TypeSplit.Room),
                new Split("Storage 2", MainManager.Maps.FactoryStorageMaze, TypeSplit.Room),
                new Split("Elevator", MainManager.Maps.FactoryStorageElevator, TypeSplit.Room),
                new Split("Lobby", MainManager.Maps.HoneyFactoryEntrance, TypeSplit.Room, endID: MainManager.Maps.HoneyFactoryCore)
            };
        }
    }
}
