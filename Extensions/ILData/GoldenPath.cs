using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class GoldenPath : SplitGroup
    {
        public GoldenPath(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            offset = TimeSpan.Parse("00:01:05.000");
            ilType = IL.GoldenPath;
            startMap = MainManager.Maps.BOGoldenPath;
            startPos = new Vector3(0, 0, 4.5f);
            items = new List<int> { (int)MainManager.Items.Mushroom, (int)MainManager.Items.HoneyDrop, (int)MainManager.Items.VitalitySeed };
            keyItems = new List<int> { 27 };
            crystalBerryAmount = 3;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 0 },
                new List<int>(){ 0 },
                new List<int>(){ 11,0 }
            };

            flags = new List<int>{7,10,11,13,14,15,16,17,22,23,24,27,28,29,30,31,32,33,34,35,36,41,74,108,691,694};

            cbFlags = new List<int> { 0,1, 2 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2},
            };

            money = 20;
            tp = 10;
            maxTp = 10;
            bp = 5;
            maxbp = 5;
            level = 1;
            exp = 50;
            maxExp = 100;
            inventorySpace = 10;

            discoveries = new List<int> { 0, 1, 3 };
            seenAreas = new List<int> { 0, 2 };
            shadesPool = null;
        }

        public override void DoSpecifics()
        {
            MainManager.instance.flagvar[53] = 0;
            MainManager.instance.flagvar[54] = 0;
            MainManager.instance.flagvar[58] = 0;
        }

        public override void SetSplits()
        {
            var waspTrooper = new MainManager.Enemies[] { MainManager.Enemies.WaspTrooper };
            splits =  new List<Split>
            {
                new Split("Reach Throne", MainManager.Maps.BugariaOutskirtsOutsideCity, TypeSplit.Room),
                new Split("Leave Palace", MainManager.Maps.AntPalace2, TypeSplit.Room),
                new Split("Quests", MainManager.Maps.AntBridge, TypeSplit.Room),
                new Split("Bad Book", MainManager.Maps.BugariaResidential, TypeSplit.Room),
                new Split("Cooking", MainManager.Maps.BugariaCommercial, TypeSplit.Room),
                new Split("Spicies", MainManager.Maps.BugariaMainPlaza, TypeSplit.Room),
                new Split("Wasp Trooper", MainManager.Maps.BOGoldenPath, TypeSplit.BattleEnd, enemies:waspTrooper),
                new Split("Cave", MainManager.Maps.GoldenPathTunnel, TypeSplit.Room),
                new Split("Crystal Berry", MainManager.Maps.GoldenHillsCableCar, TypeSplit.Room),
                new Split("Cranks", MainManager.Maps.GoldenHillsPath2, TypeSplit.Room),
                new Split("Respawn", MainManager.Maps.GoldenHillsPath3, TypeSplit.Room,endID: MainManager.Maps.GoldenSettlementEntrance)
            };
        }
    }
}
