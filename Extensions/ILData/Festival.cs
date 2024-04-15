using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Extensions.ILData
{
    public class Festival : SplitGroup
    {
        public Festival(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            ilType = IL.Festival;
            startMap = MainManager.Maps.GoldenHillsPath3;
            startPos = new Vector3(-63.5f, 0, -2.4f);
            items = new List<int> 
            { 
                (int)MainManager.Items.HoneyDrop, (int)MainManager.Items.VitalitySeed,
                (int)MainManager.Items.VitalitySeed, (int)MainManager.Items.VitalitySeed, 
                (int)MainManager.Items.VitalitySeed,(int)MainManager.Items.GenerousSeed, (int)MainManager.Items.GenerousSeed 
            };
            keyItems = new List<int> { (int)MainManager.Items.ExplorerPermit, (int)MainManager.Items.Map };

            crystalBerryAmount = 5;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 0 },
                new List<int>(){ 12,1,2,4,33,49,56},
                new List<int>(){ 11,0 }
            };

            flags = new List<int>
            {
                1,2,3,7,10,11,13,14,15,16,17,22,23,24,27,28,29,30,31,32,33,34,35,36,41,44,50,66,67,68,73,74,107,
                108,114,115,119,120,240,479,579,617,621,691,694
            };

            cbFlags = new List<int> { 0,1, 2, 5, 8 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2},
            };

            money = 9;
            tp = 10;
            maxTp = 10;
            bp = 5;
            maxbp = 5;
            level = 1;
            exp = 65;
            maxExp = 100;
            inventorySpace = 10;
            discoveries = new List<int> { 0, 1, 6, 7, 11 };
            seenAreas = new List<int> { 0, 1, 2, 5 };
            shadesPool = null;
        }

        public override void DoSpecifics()
        {
            MainManager_Ext.togglePerfectRNG = true;        
        }

        public override void SetSplits()
        {
            var enemies = new MainManager.Enemies[] { MainManager.Enemies.Acolyte };
            splits = new List<Split>
            {
                new Split("Hi Tanjy", MainManager.Maps.GoldenSettlementEntrance, TypeSplit.Room),
                new Split("Start Festival", MainManager.Maps.GoldenSettlement1, TypeSplit.Room),
                new Split("Cooking", MainManager.Maps.GoldenSettlement1Night, TypeSplit.Room),
                new Split("Eating Comp", MainManager.Maps.GoldenSettlement2Night, TypeSplit.Flag, flag:95),
                new Split("Worms", MainManager.Maps.GoldenSettlement2Night, TypeSplit.Flag, flag:96),
                new Split("Get CB", MainManager.Maps.GoldenSettlement1Night, TypeSplit.Room),
                new Split("Aria", MainManager.Maps.GoldenSettlement1Night, TypeSplit.BattleEnd, enemies: enemies, endID: MainManager.Maps.GoldenSettlement1Night)
            };
        }
    }
}
