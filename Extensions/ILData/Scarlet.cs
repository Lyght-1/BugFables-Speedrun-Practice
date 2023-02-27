using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Extensions.ILData
{
    public class Scarlet : SplitGroup
    {
        public Scarlet(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            offset = TimeSpan.Parse("00:00:39.590");
            ilType = IL.Scarlet;
            startMap = MainManager.Maps.GoldenHillsDungeonBoss;
            startPos = new Vector3(0, -2, -13.8f);
            items = new List<int> { };
            keyItems = new List<int> { 27, 41, 174 };
            crystalBerryAmount = 7;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 7,8,9,10,21,23,38,50 },
                new List<int>(){ 12,1,2,4,33,49,56},
                new List<int>(){ 11,12,0 }
            };

            flags = new List<int>
            {
                1,3,10,11,15,16,17,21,24,27,28,30,31,32,41,44,50,56,64,66,67,68,73,74,76,84,85,86,88,90,93,94,95,96,97,98,100,101,102,103,104,107,
                108,109,110,111,112,113,114,115,116,117,118,119,120,122,124,125,126,127,128,129,240,479,617,663,691,694
            };

            cbFlags = new List<int> { 0, 2, 4, 5, 8, 9 };

            medals = new List<int[]>
            {
                new int[2]{11,-1},
                new int[2]{0, 0},
                new int[2]{24,-2},
            };

            money = 13;
            tp = 16;
            maxTp = 16;
            bp = 3;
            maxbp = 5;
            level = 3;
            exp = 16;
            maxExp = 102;
            inventorySpace = 10;
            discoveries = new List<int> { 0, 1,6 ,7,11,12,13};
            seenAreas = new List<int> { 0, 1,2,4,5,6 };
            shadesPool = new List<int> { 9,6,42,19,43 };
        }

        public override void DoSpecifics()
        {
            MainManager.Heal(true,true);
            MainManager_Ext.togglePerfectRNG = true;
            MainManager.instance.flagvar[19] = 1;
            MainManager.instance.flagvar[18] = 1;
            MainManager.instance.flagvar[53] = 1;
            MainManager.instance.flagvar[54] = 5;
            MainManager.instance.flagvar[58] = 0;
            MainManager.instance.flagvar[55] = 3;
        }

        public override void SetSplits()
        {
            var enemies = new MainManager.Enemies[] { MainManager.Enemies.Scarlet };
            splits = new List<Split>
            {
                new Split("Reach Mines", MainManager.Maps.GoldenHillsDungeonUpperMain, TypeSplit.Room),
                new Split("Leave Castle", MainManager.Maps.AntTunnels, TypeSplit.Room),
                new Split("Get Quests", MainManager.Maps.AntBridge, TypeSplit.Room),
                new Split("Levi & Celia", MainManager.Maps.BugariaCommercial, TypeSplit.Room),
                new Split("Spicies", MainManager.Maps.BugariaMainPlaza, TypeSplit.Room),
                new Split("Reach Tunnel", MainManager.Maps.BOGoldenPath, TypeSplit.Room),
                new Split("Scarlet", MainManager.Maps.GoldenPathTunnel, TypeSplit.BattleEnd, enemies:enemies, endID:MainManager.Maps.GoldenPathTunnel)
            };
        }
    }
}
