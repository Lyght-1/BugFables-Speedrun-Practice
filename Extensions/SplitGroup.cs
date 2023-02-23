using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpeedrunPractice.Extensions
{
    public enum SplitState
    {
        NotStarted,
        Started,
        Ended,
        Paused,
    }
    public abstract class SplitGroup
    {
        public TimeSpan offset = TimeSpan.Zero;
        public List<Split> splits = new List<Split>();
        public string name;
        public IL ilType;
        public TypeSplit startSplit;
        public MainManager.Maps startMap;
        public Vector3 startPos;
        public List<int> items;
        public List<int> keyItems;
        public List<int[]> medals;
        public List<int> seenAreas;
        public List<int> discoveries;
        public int money;
        public int tp;
        public int maxTp;
        public int bp;
        public int maxbp;
        public int level;
        public int exp;
        public int maxExp;
        public List<int> flags;
        public List<int> cbFlags;
        public int inventorySpace;
        public List<int> shadesPool;
        public List<int>[] boardQuests;
        public int crystalBerryAmount;
        public int attemptsCount = 0;
        public SplitState state = SplitState.NotStarted;
        public SplitGroup(List<Split> splits)
        {
            this.splits = splits;
            startSplit = TypeSplit.Room;
            SetupData();
            name = ilType.ToString();
        }

        public abstract void SetupData();

        public abstract void SetSplits();

        public abstract void DoSpecifics();
    }
}
