using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class Snakemouth : SplitGroup
    {
        public Snakemouth(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            offset = TimeSpan.Parse("00:00:52.100");
            ilType = IL.Snakemouth;
            startMap = MainManager.Maps.SnakemouthFallRoom;
            startPos = new Vector3(-49, 0, 0);
            items = new List<int> { (int)MainManager.Items.HoneyDrop, (int)MainManager.Items.Mushroom };
            keyItems = new List<int> { 27 };
            crystalBerryAmount = 1;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 0 },
                new List<int>(){ 11,0 },
                new List<int>(){ 0}
            };

            flags = new List<int>{ 7,10,11,13,14,15,17,22,27,28,30,31,32,108,691,694 };

            cbFlags = new List<int> { 0 };

            medals = new List<int[]>
            {
                new int[2]{11,-2}
            };

            money = 10;
            tp = 10;
            maxTp = 10;
            bp = 5;
            maxbp = 5;
            level = 1;
            exp = 0;
            maxExp = 100;
            inventorySpace = 10;

            discoveries = new List<int> { 0, 1 };
            seenAreas = new List<int> { 2 };
            shadesPool = null;
        }

        public override void DoSpecifics()
        {
            MainManager.instance.flagvar[12] = 0;
            MainManager.instance.extrafollowers.Add(2);
        }

        public override void SetSplits()
        {
            splits = new List<Split>
            {
                new Split("Lake Room", MainManager.Maps.SnakemouthLake, TypeSplit.Room),
                new Split("Big Door", MainManager.Maps.SnakemouthUndergrondDoor, TypeSplit.Room),
                new Split("Right A", MainManager.Maps.SnakemouthUndergroundRightA, TypeSplit.Room),
                new Split("Right B", MainManager.Maps.SnakemouthUndergroundRightB, TypeSplit.Room),
                new Split("Big Door 2", MainManager.Maps.SnakemouthUndergrondDoor, TypeSplit.Room),
                new Split("Left A", MainManager.Maps.SnakemouthUndergroundLeftA, TypeSplit.Room),
                new Split("Left B", MainManager.Maps.SnakemouthUndergroundLeftB, TypeSplit.Room),
                new Split("Big Door 3", MainManager.Maps.SnakemouthUndergrondDoor, TypeSplit.Room),
                new Split("Mushrooom Pit", MainManager.Maps.SnakemouthMushroomPit, TypeSplit.Room,endID: MainManager.Maps.SnakemouthTreasureRoom)
            };
        }
    }
}
