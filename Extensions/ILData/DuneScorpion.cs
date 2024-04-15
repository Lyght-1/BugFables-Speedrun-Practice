using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpeedrunPractice.Extensions;

namespace SpeedrunPractice.Extensions.ILData
{
    public class DuneScorpion : SplitGroup
    {
        public DuneScorpion(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            offset = TimeSpan.Parse("00:00:25.300");
            ilType = IL.DuneScorpion;
            startMap = MainManager.Maps.HideoutEntrance;
            startPos = new Vector3(-5.28f, 0, -0.2f);
            items = new List<int> { 70, 70, (int)MainManager.Items.ShockShroom };
            keyItems = new List<int> { 27,41, (int)MainManager.Items.GHCrank, (int)MainManager.Items.SandCastleKey };

            crystalBerryAmount = 1;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,18,19,34,28,42,53,22,36,40 },
                new List<int>(){ 14,1,2,4,33,49,56,26,38,50,7,3,30 },
                new List<int>(){ 11,12,13,6 }
            };

            flags = new List<int>
            {
                1,3,4,7,8,10,11,15,16,17,18,20,21,22,23,24,27,30,31,32,41,44,47,50,56,66,67,68,73,74,75,76,84,85,86,88,90,92,93,94,
                95,96,97,98,100,101,102,103,104,107,108,109,111,113,114,115,116,117,118,119,120,122,124,125,126,127,128,129,130,
                133,134,135,138,140,141,159,160,167,168,169,170,172,173,174,176,177,178,179,182,201,202,206,211,212,213,214,215,
                216,217,218,221,222,224,239,240,250,258,259,260,269,278,281,282,299,300,301,302,303,307,401,415,479,482,621,579,
                617,618,621,622,660,663,690,691,694,697,699
            };

            discoveries = new List<int> { 0, 1, 6, 7, 11, 12, 13, 20, 21, 24, 29 };
            seenAreas = new List<int> { 0, 1, 2, 3, 4, 5, 6, 10, 12, 13, 20 };
            cbFlags = new List<int> { 0,1, 2, 4, 5, 8, 9, 12, 15, 16, 18 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2},
                new int[2]{24,-2},
                new int[2]{6,-2},
                new int[2]{15,-2},
                new int[2]{14,-1},
                new int[2]{23,-2},
                new int[2]{49,0},
            };

            money = 28;
            tp = 16;
            maxTp = 16;
            bp = 3;
            maxbp = 11;
            level = 5;
            exp = 13;
            maxExp = 104;
            inventorySpace = 10;
            discoveries = null;
            seenAreas = null;
            shadesPool = new List<int> { 42, 43, 19, 9, 49, 0 };
        }

        public override void DoSpecifics()
        {
            MainManager.Heal(true, true);
            MainManager_Ext.togglePerfectRNG = true;
        }

        public override void SetSplits()
        {
            var duneScorp = new MainManager.Enemies[] { MainManager.Enemies.Pseudoscorpion,MainManager.Enemies.Scorpion };
            splits = new List<Split>
            {
                new Split("Leave Bandit", MainManager.Maps.DesertBadlands, TypeSplit.Room),
                new Split("Hawk Room", MainManager.Maps.DesertRockFormation, TypeSplit.Room),
                new Split("Cactiling", MainManager.Maps.DesertEastmost, TypeSplit.Room),
                new Split("Dune Scorp", MainManager.Maps.DesertScorpion, TypeSplit.BattleEnd,endID: MainManager.Maps.DesertScorpion, enemies:duneScorp)
            };
        }
    }
}
