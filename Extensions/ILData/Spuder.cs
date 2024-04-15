using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class Spuder : SplitGroup
    {
        public Spuder(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            ilType = IL.Spuder;
            startMap = MainManager.Maps.SnakemouthMushroomPit;
            startPos = new Vector3(-0.2f, 18, 79);
            items = new List<int> { (int)MainManager.Items.HoneyDrop, (int)MainManager.Items.Mushroom, (int)MainManager.Items.HoneyDrop };
            keyItems = new List<int> { 27 };
            crystalBerryAmount = 3;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 0 },
                new List<int>(){ 11,0 },
                new List<int>(){ 0}
            };

            flags = new List<int> { 7, 10, 11, 13, 14, 15,16, 17, 22,23,24, 27, 28, 29,30, 31, 32,33,34,35,36, 108, 691, 694 };

            cbFlags = new List<int> { 0,1,2 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2}
            };

            money = 15;
            tp = 10;
            maxTp = 10;
            bp = 5;
            maxbp = 5;
            level = 1;
            exp = 14;
            maxExp = 100;
            inventorySpace = 10;

            discoveries = new List<int> { 0, 1 };
            seenAreas = new List<int> { 2 };
            shadesPool = null;
        }

        public override void DoSpecifics(){}

        public override void SetSplits()
        {
            var enemies = new MainManager.Enemies[] { MainManager.Enemies.SpuderReal };
            splits = new List<Split>
            {
                new Split("Spuder", MainManager.Maps.SnakemouthTreasureRoom, TypeSplit.BattleEnd,enemies:enemies, endID:MainManager.Maps.SnakemouthTreasureRoom),
            };
        }
    }
}
