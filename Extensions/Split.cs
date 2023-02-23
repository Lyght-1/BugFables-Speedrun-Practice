using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.Globalization;

namespace SpeedrunPractice.Extensions
{
    public enum TypeSplit
    {
        Room,
        BattleStart,
        BattleEnd,
        Flag,
        Credit,
        None
    }

    public class Split
    {
        public string name;
        public MainManager.Maps roomID;
        public TypeSplit type;
        public TimeSpan runTime;
        public TimeSpan goldTime;
        public TimeSpan pbTime;
        public MainManager.Maps endID;
        public TimeSpan segmentTime;
        public MainManager.Enemies[] battleEnemies;
        public TimeSpan oldGold;
        public TimeSpan oldPB;
        public TimeSpan oldSegmentTime;
        public int flagID;

        public Split(string name, MainManager.Maps roomID, TypeSplit type, MainManager.Maps endID = 0, MainManager.Enemies[] enemies=null, int flag = -1)
        {
            this.name = name;
            this.roomID = roomID;
            this.type = type;
            this.endID = endID;
            flagID = flag;
            battleEnemies = enemies;
            runTime = TimeSpan.Zero;
            goldTime = TimeSpan.Zero;
            oldGold = TimeSpan.Zero;
            oldPB = TimeSpan.Zero;
            pbTime = TimeSpan.Zero;
            segmentTime = TimeSpan.Zero;
            oldSegmentTime = TimeSpan.Zero;
        }

        public void SetTimes(TimeSpan segmentTime, TimeSpan goldTime, TimeSpan pbTime)
        {
            this.segmentTime = segmentTime;
            this.goldTime = goldTime;
            oldGold = goldTime;
            this.pbTime = pbTime;
            oldPB = pbTime;
            oldSegmentTime = segmentTime;
        }

        public void EndSplit(TimeSpan timeEnd)
        {
            ILTimer ilTimer = MainManager.instance.GetComponent<ILTimer>();
            runTime = timeEnd;

            if(ilTimer.splitIndex != 0)
            {
                segmentTime = runTime - ilTimer.splitGroups[(int)ilTimer.il].splits[ilTimer.splitIndex-1].runTime;
            }
            else
            {
                segmentTime = runTime;
            }

            if (segmentTime < goldTime || goldTime == TimeSpan.Zero)
            {
                goldTime = segmentTime; //issa gold
                Console.WriteLine("GOLDED");
                MainManager.PlaySound("LevelUp");
            }

            if (endID != 0)
            {
                if (runTime < pbTime || pbTime == TimeSpan.Zero)
                {
                    pbTime = runTime; //issa pb
                    oldGold = goldTime;
                    foreach (var split in ilTimer.splitGroups[(int)ilTimer.il].splits)
                    {
                        split.pbTime = split.runTime;
                        split.oldGold = split.goldTime;
                        split.oldSegmentTime = split.segmentTime;
                    }
                    ilTimer.RecordGhostPb();
                    ilTimer.WriteSplits();
                    ilTimer.WriteGhostRecordings();
                    MainManager.instance.StartCoroutine(DoPBAnim());
                }
            }
        }

        public IEnumerator DoPBAnim()
        {
            yield return new WaitUntil(()=>!MainManager.roomtransition);
            if (MainManager.instance.discoverymessage.childCount == 4)
                MainManager.instance.StartCoroutine(MainManager.SetText("|rainbow|New PB !", 1, null, false, false, new Vector3(1.2f, -1.2f, 10f), Vector3.zero, new Vector2(1.2f, 1.2f), MainManager.instance.discoverymessage, null));
            for (int i = 1; i != 4; i++)
                MainManager.instance.discoverymessage.GetChild(i).gameObject.SetActive(false);
            MainManager.instance.discoverymessage.GetChild(4).gameObject.SetActive(true);
            MainManager.instance.discoveryhud = 350f;
            MainManager.instance.discoverymessage.GetComponentInChildren<Animator>().PlayInFixedTime("Arch");

        }

        public override string ToString()
        {
            string pb = GetTimeFormatHours(pbTime);
            string segment = GetTimeFormatHours(segmentTime);
            string gold = GetTimeFormatHours(goldTime);
            return name + "," + pb + "," + segment + "," + gold;
        }

        public static string GetTimeFormatHours(TimeSpan time)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            return string.Format("{0:D2}:{1:D2}:{2:D2}.{3}",time.Hours, time.Minutes, time.Seconds, time.Milliseconds.ToString().PadLeft(3, '0'));
        }

        public static string GetTimeFormat(TimeSpan time)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            return string.Format("{0:D2}:{1:D2}.{2}", time.Minutes, time.Seconds, time.Milliseconds.ToString().PadLeft(3, '0'));
        }

        public void UndoSplit()
        {
            goldTime = oldGold;
            segmentTime = oldSegmentTime;
        }
    }
}
