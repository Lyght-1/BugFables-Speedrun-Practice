using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class LeifRescue : SplitGroup
    {
        public LeifRescue(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            ilType = IL.LeifRescue;
            startMap = MainManager.Maps.OutsideSnakemouth;
            startPos = new Vector3(-23, 1.1f, 12);
            items = new List<int> { (int)MainManager.Items.HoneyDrop };
            keyItems = new List<int> { 27 };
            crystalBerryAmount = 1;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 0 },
                new List<int>(){ 11,0 },
                new List<int>(){ 0}
            };

            flags = new List<int>{15,17,22,28,31,32,108,691,694};

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

            discoveries = new List<int> { 0 };
            seenAreas = new List<int>();
            shadesPool = null;
        }

        public override void DoSpecifics()
        {
            MainManager.instance.flagvar[12] = 0;
        }

        public override void SetSplits()
        {
            var spuder1 = new MainManager.Enemies[] { MainManager.Enemies.Spuder };
            var spuder2 = new MainManager.Enemies[] { MainManager.Enemies.Spuder, MainManager.Enemies.MothWeb };
            splits = new List<Split>
            {
                new Split("Bridge Room", MainManager.Maps.SnakemouthBridgeRoom, TypeSplit.Room),
                new Split("Ability Jump", MainManager.Maps.SnakemouthDoorRoom, TypeSplit.Room),
                new Split("Spuder 1", MainManager.Maps.SnakemouthFallRoom, TypeSplit.BattleEnd, enemies:spuder1),
                new Split("Spuder 2", MainManager.Maps.SnakemouthFallRoom, TypeSplit.BattleEnd, enemies:spuder2,endID:MainManager.Maps.SnakemouthFallRoom)
            };
        }
    }
}
