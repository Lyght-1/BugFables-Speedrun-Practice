using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Extensions.ILData
{
    public class Termite : SplitGroup
    {
        public Termite(List<Split> splits) : base(splits) { }

        public override void SetupData()
        {
            ilType = IL.Termite;
            startSplit = TypeSplit.BattleEnd;
            startMap = MainManager.Maps.BarrenLandsMiniboss;
            startPos = new Vector3(7.2f, 0, -2f);
            items = new List<int> { (int)MainManager.Items.BlackCherry, (int)MainManager.Items.BlackCherry };
            keyItems = new List<int> { 27, 41, (int)MainManager.Items.GHCrank, 116 };
            crystalBerryAmount = 0;

            boardQuests = new List<int>[]
            {
                new List<int>(){ 8,9,10,21,23,18,19,34,28,42,53,22,36,40,25,5,35,0 },
                new List<int>(){ 15,1,2,4,33,49,56,26,38,50,7,3,30 },
                new List<int>(){ 11,12,13,14,6 }
            };

            flags = new List<int>
            {
                1,10,11,15,16,17,18,19,20,21,22,24,27,28,29,30,31,32,38,39,41,56,66,67,68,73,74,75,76,80,84,85,86,88,90,93,94,95,96,97,98,100,103,104,
                108,114,115,119,120,122,124,125,126,130,133,138,140,141,159,160,167,168,169,170,172,173,174,176,177,178,179,201,202,206,211,212,213,
                214,215,216,217,218,221,222,224,239,250,258,259,260,269,278,280,281,282,284,288,289,290,291,292,293,294,295,296,297,298,299,300,
                301,302,303,304,306,307,322,323,334,335,336,337,345,347,348,354,357,359,363,364,365,366,367,368,369,370,371,372,374,376,377,383,
                401,415,416,420,479,482,579,617,621,622,642,657,660,663,691,694,697,699,708,
            };

            cbFlags = new List<int> { 0,1, 2, 4, 5, 8, 9, 12, 13, 15, 16, 18, 29, 43, 44 };

            medals = new List<int[]>
            {
                new int[2]{11,-2},
                new int[2]{0,-2},
                new int[2]{24,0},
                new int[2]{6,0},
                new int[2]{6,0},
                new int[2]{15,-1},
                new int[2]{14,-2},
                new int[2]{49,0},
                new int[2]{23,1}
            };

            money = 80;
            tp = 16;
            maxTp = 16;
            bp = 1;
            maxbp = 17;
            level = 7;
            exp = 39;
            maxExp = 106;
            inventorySpace = 10;

            discoveries = new List<int> { 0, 1, 6, 7, 11, 12, 13, 16, 20, 21, 24, 27, 29, 31, 33, 36, 37 };
            seenAreas = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 19, 20 };
            shadesPool = null;
        }

        public override void DoSpecifics()
        {
            MainManager.instance.extrafollowers.Add(96);
            MainManager_Ext.togglePerfectRNG = true;
            MainManager.Heal(true, true);
        }

        public override void SetSplits()
        {
            var primalWeevil = new MainManager.Enemies[] { MainManager.Enemies.PrimalWeevil };
            var plumpling = new MainManager.Enemies[] { MainManager.Enemies.Plumpling, MainManager.Enemies.MimicSpider };
            var crossPoi = new MainManager.Enemies[] { MainManager.Enemies.TermiteSoldier, MainManager.Enemies.TermiteNasute };
            var zaspMothiva = new MainManager.Enemies[] { MainManager.Enemies.Zasp, MainManager.Enemies.Mothiva1 };
            splits = new List<Split>
            {
                new Split("Primal", MainManager.Maps.BarrenLandsMiniboss, TypeSplit.Room, enemies:primalWeevil),
                new Split("Big Door", MainManager.Maps.TermiteOutside, TypeSplit.Room),
                new Split("Main Plaza", MainManager.Maps.TermiteMainPlaza, TypeSplit.Room),
                new Split("Throne", MainManager.Maps.TermiteRoyalChamber, TypeSplit.Room),
                new Split("Main Plaza 2", MainManager.Maps.TermiteMainPlaza, TypeSplit.Room),
                new Split("Reach Colo", MainManager.Maps.TermiteIndustrial, TypeSplit.Room),
                new Split("Plumpling", MainManager.Maps.TermiteColiseum1, TypeSplit.BattleEnd, enemies:plumpling),
                new Split("Cross & Poi", MainManager.Maps.TermiteColiseum2, TypeSplit.BattleEnd, enemies:crossPoi),
                new Split("Z&M 2", MainManager.Maps.TermiteColiseum2, TypeSplit.BattleEnd, endID:MainManager.Maps.TermiteColiseum2, enemies:zaspMothiva)
            };
        }
    }
}
