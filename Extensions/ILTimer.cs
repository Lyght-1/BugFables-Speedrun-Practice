using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using UnityEngine;

namespace SpeedrunPractice.Extensions
{
    public enum IL
    {
        LeifRescue,
        Snakemouth,
        Spuder,
        GoldenPath,
        Festival,
        GoldenHills,
        ZaspMothiva,
        UpperGolden,
        VeGu,
        Scarlet,
        MerchantsRescue,
        EnterFactory,
        HoneyFactory,
        Abomihoneys,
        Ahoneynation,
        EnterCore,
        B33,
        EnterBH,
        BanditHideout,
        Astotheles,
        DuneScorpion,
        SandCastle,
        Watcher,
        EnterFG,
        Swamplands,
        Beast,
        GeneralUltimax,
        EnterForsaken,
        PrimalWeevil,
        Termite,
        EnterPrison,
        RubberPrison,
        UltimaxTank,
        Deadlands,
        Deadlanders,
        Fridge,
        Oven,
        WaspKing,
        None,
    }
    public class ILTimer : MonoBehaviour
    {
        public static bool showTimer = false;
        public Transform clock;
        public DynamicFont timerUI;
        public DynamicFont currentSplitName;
        public DynamicFont currentSplitGold;
        public DynamicFont currentSplitPB;
        public DynamicFont previousSplitComp;
        public DynamicFont currentSplitTime;
        public static bool start = false;
        public static float ghostOpacity = 0.51f;
        float timer = 0f;
        public IL il = IL.None;
        public int splitIndex = 0;
        public SplitGroup[] splitGroups = new SplitGroup[Enum.GetNames(typeof(IL)).Length - 1];
        public EntityControl ghost;
        public List<GhostRecorder> recordings = new List<GhostRecorder>();
        public GhostRecorder[] pbGhosts = null;
        public static bool hideGhost = false;
        int pbGhostIndex = 0;
        public static bool lvlSongStarted = false;
        public static bool endCredit = false;
        int frameStart = 0;
        void Start()
        {
            timerUI = DynamicFont.SetUp(true, 1f, 2, 100, Vector2.one * 1.2f, MainManager.GUICamera.transform, new Vector3(-7.6f, 2.4f, 10f));
            timerUI.dropshadow = true;
            timerUI.gameObject.SetActive(false);
            TimeSpan time = TimeSpan.FromSeconds(timer);
            timerUI.text = Split.GetTimeFormat(time);

            clock = MainManager.NewUIObject("clock", timerUI.transform, new Vector3(-0.6f, 0.4f, 10f), new Vector3(0.7f, 0.7f, 0.7f), MainManager.guisprites[84], 100).transform;
            clock.gameObject.AddComponent<SpriteBounce>().MessageBounce();
            clock.gameObject.GetComponent<SpriteBounce>().basescale = new Vector3(0.7f, 0.7f, 0.7f);

            currentSplitName = DynamicFont.SetUp(true, 20f, 2, 100, new Vector2(0.4f, 0.4f), timerUI.transform, new Vector3(0f, 0.8f, 10f));

            currentSplitGold = DynamicFont.SetUp(true, 20f, 2, 100, new Vector2(0.35f, 0.35f), timerUI.transform, new Vector3(0f, -0.45f, 10f));

            currentSplitPB = DynamicFont.SetUp(true, 20f, 2, 100, new Vector2(0.35f, 0.35f), timerUI.transform, new Vector3(0f, -0.2f, 10f));

            previousSplitComp = DynamicFont.SetUp(true, 1f, 2, 100, new Vector2(0.35f, 0.35f), timerUI.transform, new Vector3(2.2f, -0.2f, 10f));

            currentSplitTime = DynamicFont.SetUp(true, 1f, 2, 100, new Vector2(0.35f, 0.35f), timerUI.transform, new Vector3(2.4f, 0.8f, 10f));

            ReadSplits();
        }

        void Update()
        {
            if (il != IL.None)
            {
                if (start && MainManager_Ext.ilMode)
                {
                    pbGhostIndex = Time.frameCount - frameStart;
                    var playerEntity = MainManager.player.entity;
                    if (playerEntity != null)
                        recordings.Add(new GhostRecorder(playerEntity.transform.position, playerEntity.animstate, playerEntity.flip, playerEntity.animid, MainManager.map.mapid));
                    timer += Time.unscaledDeltaTime;
                    TimeSpan time = TimeSpan.FromSeconds(timer);
                    timerUI.text = Split.GetTimeFormat(time);

                    TimeSpan currentSplitTimer = time;
                    if (splitIndex != 0)
                    {
                        currentSplitTimer = time.Subtract(splitGroups[(int)il].splits[splitIndex - 1].runTime);
                    }
                    if (GetCurrentSplit().segmentTime != TimeSpan.Zero && currentSplitTimer.CompareTo(GetCurrentSplit().segmentTime) == 1 && currentSplitTime.fontcolor == Color.white)
                    {
                        currentSplitTime = ChangeDynamicFontColor(currentSplitTime, Color.red, "");
                    }
                    else
                    {
                        if (currentSplitTime.fontcolor == Color.red && currentSplitTimer.CompareTo(GetCurrentSplit().segmentTime) == -1)
                        {
                            currentSplitTime = ChangeDynamicFontColor(currentSplitTime, Color.white, "");
                        }
                        currentSplitTime.text = Split.GetTimeFormat(currentSplitTimer);
                    }

                    if (pbGhosts != null && pbGhosts.Length != 0 && pbGhostIndex < pbGhosts.Length)
                    {
                        var currentGhost = pbGhosts[pbGhostIndex];
                        if (ghost == null && MainManager.instance.playerdata != null && MainManager.instance.playerdata[0].entity != null)
                        {
                            CreateGhost();
                        }

                        if (ghost != null)
                        {
                            //this is cause for 2 frames u can see the ghost in a weird pos after map transition, cause game does loadmap before moving players
                            bool ghostNotInTransition = pbGhostIndex > 1 && currentGhost.currentMap == pbGhosts[pbGhostIndex-2].currentMap;

                            if (MainManager.map.mapid == currentGhost.currentMap && !hideGhost && !MainManager.instance.inbattle && ghostNotInTransition)
                            {
                                ChangeGhostOpacity();
                                ghost.gameObject.SetActive(true);
                                ghost.SetPosition(currentGhost.position);
                                ghost.SetState(currentGhost.animState);
                                ghost.flip = currentGhost.flip;
                                ghost.animid = currentGhost.animID;

                            }
                            else
                            {
                                ghost.gameObject.SetActive(false);
                            }
                        }
                    }
                    if (GetCurrentSplit().type == TypeSplit.Flag)
                        CheckSplit();

                    if (GetCurrentSplit().type == TypeSplit.Credit && !endCredit)
                    {
                        if (MainManager.instance.inevent && MainManager.lastevent == 205 && MainManager.map.mapid == MainManager.Maps.BugariaEndThrone)
                        {
                            if (MainManager.musicids[0] == (int)MainManager.Musics.LevelUp)
                            {
                                lvlSongStarted = true;
                            }
                        }
                    }
                }
            }
        }

        void ShowTimerUI()
        {
            showTimer = true;
            timerUI.gameObject.SetActive(true);
            currentSplitName.text = splitGroups[(int)il].splits[0].name;

            currentSplitGold.text = "Gold: " + Split.GetTimeFormat(splitGroups[(int)il].splits[0].goldTime);

            currentSplitPB.text = "PB:   " + Split.GetTimeFormat(splitGroups[(int)il].splits[0].oldSegmentTime);

            currentSplitTime = ChangeDynamicFontColor(currentSplitTime, Color.white, timerUI.text);
            ChangeUI(GetCurrentSplit(), GetCurrentSplit());
        }

        public IEnumerator StartIL(IL ilType)
        {
            MainManager_Ext.ilMode = true;
            MainManager_Ext.noFreezeRes = ilType != IL.Oven;
            il = ilType;
            var splitGroup = splitGroups[(int)il];
            MainManager.instance.minipause = true;
            MainManager.instance.inevent = true;
            MainManager.player.CancelAction();
            MainManager.StopEntitiesMove();
            ResetTimer();
            endCredit = false;
            lvlSongStarted = false;
            splitGroup.state = SplitState.NotStarted;

            recordings.Clear();
            LoadGhost();
            pbGhostIndex = 0;

            GC.Collect();
            Resources.UnloadUnusedAssets();

            foreach (var sound in MainManager.sounds)
            {
                if(sound != null)
                    MainManager.StopSound(sound.clip);
            }

            if (il == IL.LeifRescue || il == IL.Snakemouth)
            {
                MainManager.ChangeParty(new int[] { 0, 1 }, true, false);
            }
            else
            {
                MainManager.ChangeParty(new int[] { 0, 1, 2}, true, false);
            }

            for (int i = 0; i < MainManager.instance.playerdata.Length; i++)
            {
                if (MainManager.instance.playerdata[i].entity != null)
                {
                    Destroy(MainManager.instance.playerdata[i].entity.gameObject);
                }
            }
            MainManager.SetPlayers();
            SetFlags(splitGroup);

            var EndEventRef = AccessTools.Method(typeof(EventControl), "EndEvent");
            yield return StartCoroutine(MainManager.TransferMap((int)splitGroup.startMap, splitGroup.startPos));
            yield return new WaitUntil(() => !MainManager.roomtransition);
            MainManager.ChangeMusic();
            EndEventRef.Invoke(MainManager.events, null);
            yield return null;
            ShowTimerUI();

            if (il == IL.RubberPrison)
            {
                //just so it can get destroyed in event153
                GameObject test = new GameObject();
                test.transform.parent = MainManager.player.entity.sprite.transform;
            }

            for (int i = 0; i < MainManager.instance.playerdata.Length; i++)
            { 
                MainManager.instance.playerdata[i].entity.startscale = Vector3.one;
            }
        }

        void SetFlags(SplitGroup splitGroup)
        {
            for (int i = 0; i != MainManager.instance.flags.Length; i++)
                MainManager.instance.flags[i] = false;

            foreach (var neededFlag in splitGroup.flags)
                MainManager.instance.flags[neededFlag] = true;

            for (int i = 0; i != MainManager.instance.crystalbflags.Length; i++)
                MainManager.instance.crystalbflags[i] = false;

            foreach (var neededCBFlag in splitGroup.cbFlags)
                MainManager.instance.crystalbflags[neededCBFlag] = true;


            MainManager.instance.partylevel = splitGroup.level;
            MainManager.instance.partyexp = splitGroup.exp;
            MainManager.instance.neededexp = splitGroup.maxExp;
            MainManager.instance.badges.Clear();

            foreach (var medal in splitGroup.medals)
                MainManager.instance.badges.Add(new int[] { medal[0], medal[1] });

            //items
            //normal items
            MainManager.instance.items[0].Clear();
            MainManager.instance.items[0].AddRange(splitGroup.items);
            //key items
            MainManager.instance.items[1].Clear();
            MainManager.instance.items[1].AddRange(splitGroup.keyItems);

            MainManager.instance.items[2].Clear();

            MainManager.instance.statbonus.Clear();

            MainManager.instance.money = splitGroup.money;
            MainManager.instance.maxbp = splitGroup.maxbp;
            MainManager.instance.bp = splitGroup.bp;

            for (int i = 10; i != splitGroup.maxTp; i += 3)
                MainManager.AddStatBonus(MainManager.StatBonus.TP, 3, -1);

            for (int i = 5; i != splitGroup.maxbp; i += 3)
                MainManager.AddStatBonus(MainManager.StatBonus.MP, 3, -1);

            MainManager.instance.basetp = splitGroup.tp;
            MainManager.instance.tp = splitGroup.tp;
            MainManager.instance.tpt = splitGroup.maxTp;
            MainManager.instance.maxtp = splitGroup.maxTp;

            MainManager.instance.extrafollowers = new List<int>();

            for (int i = 0; i != MainManager.instance.playerdata.Length; i++)
                SetStats(i);

            MainManager.instance.librarystuff = new bool[5, 256];
            if (splitGroup.discoveries != null)
            {
                foreach(var discovery in splitGroup.discoveries)
                {
                    MainManager.instance.librarystuff[(int)MainManager.Library.Discovery, discovery] = true;
                }
            }

            if (splitGroup.seenAreas != null)
            {
                foreach (var seenArea in splitGroup.seenAreas)
                {
                    MainManager.instance.librarystuff[(int)MainManager.Library.Map, seenArea] = true;
                }
            }

            if(splitGroup.boardQuests != null)
            {
                MainManager.instance.boardquests = new List<int>[3];
                for (int i=0; i< MainManager.instance.boardquests.Length; i++)
                {
                    MainManager.instance.boardquests[i] = new List<int>();
                    foreach (var quest in splitGroup.boardQuests[i])
                    {
                        MainManager.instance.boardquests[i].Add(quest);
                    }
                }
            }

            MainManager.instance.regionalflags = new bool[MainManager.instance.regionalflags.Length];

            MainManager.instance.maxitems = splitGroup.inventorySpace;

            if(splitGroup.shadesPool != null)
            {
                MainManager.instance.badgeshops[1].Clear();
                foreach(var medal in splitGroup.shadesPool)
                {
                    MainManager.instance.badgeshops[1].Add(medal);
                }
            }
            MainManager.instance.flagvar[14] = splitGroup.crystalBerryAmount;
            MainManager.instance.flagvar[22] = 0;

            MainManager.ApplyBadges();
            splitGroup.DoSpecifics();
        }

        void SetStats(int id)
        {
            int hp = id == 1 ? 9 : 7;
            MainManager.instance.playerdata[id].hp = hp;
            MainManager.instance.playerdata[id].atk = 2;
            MainManager.instance.playerdata[id].def = 0;
            MainManager.instance.playerdata[id].maxhp = hp;
        }

        public void CheckStart(int idRoom = 0, int[] enemies = null)
        {
            if (il != IL.None && !start && splitIndex == 0 && splitGroups[(int)il].state == SplitState.NotStarted)
            {
                var split = GetCurrentSplit();
                if (idRoom != 0)
                {
                    if (splitGroups[(int)il].startSplit == TypeSplit.Room && idRoom == (int)split.roomID)
                    {
                        Console.WriteLine("set start to true in check start (in if typeroom)");
                        StartSplit();
                    }
                }

                if(enemies != null)
                {
                    if (splitGroups[(int)il].startSplit == TypeSplit.BattleEnd && CheckEnemiesBattleSplit(split, enemies))
                    {
                        Console.WriteLine("set start to true in check start (in if typebattleend)");
                        StartSplit();
                    }
                }
            }
        }

        void StartSplit()
        {
            start = true;
            frameStart = Time.frameCount;
            splitGroups[(int)il].state = SplitState.Started;
            splitGroups[(int)il].attemptsCount += 1;
            WriteSplits();
        }

        public void CheckSplit(int idRoom = 0)
        {
            var split = GetCurrentSplit();
            if (splitIndex == splitGroups[(int)il].splits.Count - 1)
            {
                CheckEndSplit(idRoom, null);
                return;
            }

            if(split.type == TypeSplit.Room && idRoom == (int)splitGroups[(int)il].splits[splitIndex + 1].roomID)
            {
                DoSplit(split);
            }

            if (split.type == TypeSplit.Flag && CheckFlagSplit(split.flagID))
            {
                DoSplit(split);
            }
        }

        public void CheckBattleSplit(int[] enemies, bool battleStart)
        {
            var split = GetCurrentSplit();

            if (splitIndex == splitGroups[(int)il].splits.Count - 1 && !battleStart)
            {
                CheckEndSplit(0, enemies);
                return;
            }

            if (split.type == TypeSplit.BattleStart && CheckEnemiesBattleSplit(split, enemies) && battleStart)
            {
                DoSplit(split);
                return;
            }

            if (split.type == TypeSplit.BattleEnd && CheckEnemiesBattleSplit(split, enemies) && !battleStart)
            {
                DoSplit(split);
            }
        }

        public void CheckEndSplit(int idRoom = 0, int[] enemies = null)
        {
            var split = GetCurrentSplit();
            if ((split.type == TypeSplit.Room && idRoom == (int)split.endID) || (split.type == TypeSplit.BattleEnd && CheckEnemiesBattleSplit(split, enemies)) || (split.type == TypeSplit.Flag && CheckFlagSplit(split.flagID)))
            {
                split.EndSplit(TimeSpan.FromSeconds(timer));
                ChangeUI(split, split);
                start = false;
                splitGroups[(int)il].state = SplitState.Ended;
                Console.WriteLine("set start to false in checkEndsplit");
            }
        }

        public bool CheckFlagSplit(int flag) => flag > -1 && (flag <= MainManager.instance.flags.Length - 1) && MainManager.instance.flags[flag];

        void DoSplit(Split lastSplit)
        {
            lastSplit.EndSplit(TimeSpan.FromSeconds(timer));
            splitIndex++;
            var split = GetCurrentSplit();
            ChangeUI(split,lastSplit);
        }

        void ChangeUI(Split split, Split lastSplit)
        {
            currentSplitName.text = split.name;
            currentSplitGold.text = "Gold: " + Split.GetTimeFormat(split.goldTime);
            currentSplitPB.text = "PB:   " + Split.GetTimeFormat(split.oldSegmentTime);


            TimeSpan timediff = TimeSpan.Zero;
            string aheadBehindSign = "";
            Color newColor = Color.white;

            Console.WriteLine("Current PB : " + lastSplit.pbTime + " | Run Time : " + lastSplit.runTime);
            Console.WriteLine("SplitIndex : " + splitIndex);
            if (lastSplit.pbTime != TimeSpan.Zero && lastSplit.runTime != TimeSpan.Zero)
            {
                if(lastSplit.runTime == lastSplit.pbTime)
                {
                    timediff = lastSplit.pbTime.Subtract(lastSplit.oldPB);
                    lastSplit.oldPB = lastSplit.pbTime;
                }
                else
                {
                    timediff = lastSplit.runTime.Subtract(lastSplit.pbTime);
                }

                if (timediff.TotalSeconds < 0)
                {
                    aheadBehindSign = "-";
                    newColor = Color.green;
                    timediff = timediff.Negate();
                }
                else
                {
                    aheadBehindSign = "+";
                    newColor = Color.red;
                }
                Console.WriteLine("Current Gold : " + lastSplit.goldTime + " | Old Gold : " + lastSplit.oldGold);
                if (lastSplit.goldTime.CompareTo(lastSplit.oldGold) == -1)
                {
                    newColor = new Color(0.92f, 0.73f, 0.20f);
                }
            }
 
            string previousSplitText = aheadBehindSign + Split.GetTimeFormat(timediff);
            previousSplitComp = ChangeDynamicFontColor(previousSplitComp, newColor, previousSplitText);
        }

        bool CheckEnemiesBattleSplit(Split split, int[] enemies)
        {
            if (split.battleEnemies != null && enemies != null && split.battleEnemies.Length == enemies.Length)
            {
                for (int i = 0; i != split.battleEnemies.Length; i++)
                {
                    if ((int)split.battleEnemies[i] != enemies[i])
                        return false;
                }
                return true;
            }
            return false;
        }

        public void ResetIL()
        {
            if (MainManager.instance.inbattle)
            {
                CleanBattle();
            }

            if (MainManager.instance.inevent)
            {
                MainManager.events.StopAllCoroutines();
            }

            if (MainManager.instance.message)
            {
                CleanMessage();
            }

            if (NewGold())
            {
                StartCoroutine(MainManager.SetText("Would you like to save your golds ?|prompt,yesno,-206,-207|", null, null));
                MainManager.instance.message = true;
            }
            else
            {
                ResetSplits();
            }
        }

        void CleanMessage()
        {
            MainManager.instance.StopAllCoroutines();
            if (MainManager.maintextbox != null)
            {
                Destroy(MainManager.maintextbox);
            }

            if (MainManager.instance.promptbox != null)
            {
                Destroy(MainManager.instance.promptbox);
            }

            if(MainManager.instance.itemlist != null)
            {
                Destroy(MainManager.instance.itemlist);
            }

            if (MainManager.instance.cursor != null)
            {
                Destroy(MainManager.instance.cursor);
            }

            MainManager.instance.message = false;
            MainManager.instance.minipause = true;
        }

        void CleanBattle()
        {
            var actiontextRef = AccessTools.FieldRefAccess<BattleControl, SpriteRenderer>("actiontext");
            var hexpcounterRef = AccessTools.FieldRefAccess<BattleControl, Transform>("hexpcounter");
            var helpboxRef = AccessTools.FieldRefAccess<BattleControl, DialogueAnim>("helpbox");
            var cancelbRef = AccessTools.FieldRefAccess<BattleControl, ButtonSprite>("cancelb");
            var switchiconRef = AccessTools.FieldRefAccess<BattleControl, Transform>("switchicon");

            if (actiontextRef(MainManager.battle) != null)
            {
                Destroy(actiontextRef(MainManager.battle).gameObject);
            }

            if (hexpcounterRef(MainManager.battle) != null)
            {
                Destroy(hexpcounterRef(MainManager.battle).gameObject);
            }

            if (helpboxRef(MainManager.battle) != null)
            {
                Destroy(helpboxRef(MainManager.battle).gameObject);
            }

            if (cancelbRef(MainManager.battle) != null)
            {
                Destroy(cancelbRef(MainManager.battle).gameObject);
            }

            if (switchiconRef(MainManager.battle) != null)
            {
                Destroy(switchiconRef(MainManager.battle).gameObject);
            }

            if (MainManager.battle != null)
            {
                MainManager.battle.StopAllCoroutines();
                Destroy(MainManager.battle.battlemap.gameObject);
            }
            MainManager.events.StopAllCoroutines();
            MainManager.instance.inbattle = false;
            MainManager.instance.minipause = true;
            MainManager.instance.pause = false;
            Destroy(MainManager.battle);
        }

        public void SaveGolds()
        {
            foreach (var split in splitGroups[(int)il].splits)
            {
                split.oldGold = split.goldTime;
                split.segmentTime = split.oldSegmentTime;
            }
            WriteSplits();
            StartCoroutine(StartIL(il));
        }

        public void ResetSplits()
        {
            foreach (var split in splitGroups[(int)il].splits)
            {
                split.goldTime = split.oldGold;
                split.segmentTime = split.oldSegmentTime;
            }
            StartCoroutine(StartIL(il));
        }

        public bool NewGold()
        {
            foreach (var split in splitGroups[(int)il].splits)
            {
                if (split.goldTime.CompareTo(split.oldGold) == -1)
                    return true;
            }
            return false;
        }

        void ResetTimer()
        {
            foreach (var split in splitGroups[(int)il].splits)
            {
                split.runTime = TimeSpan.Zero;
            }
            timer = (float)splitGroups[(int)il].offset.TotalSeconds;
            start = false;
            Console.WriteLine("set start to false in Resettimer");
            splitIndex = 0;
            TimeSpan time = TimeSpan.FromSeconds(timer);
            timerUI.text = Split.GetTimeFormat(time);
            if (ghost != null && ghost.gameObject.activeSelf)
            {
                Destroy(ghost.gameObject);
            }
        }

        public void CreateGhost()
        {
            ghost = EntityControl.CreateNewEntity("ghost");
            ghost.activeonpause = true;
            ghost.activeinevents = true;
            ChangeGhostOpacity();

            for(int i=0; i!= MainManager.instance.playerdata.Length; i++)
            {
                foreach (var collider in MainManager.instance.playerdata[i].entity.GetComponentsInChildren<Collider>())
                    EntityControl.IgnoreColliders(ghost, collider, true);
            }

            foreach(var entity in MainManager.map.entities)
            {
                if(entity != null)
                {
                    foreach (var collider in entity.GetComponentsInChildren<Collider>())
                        EntityControl.IgnoreColliders(ghost, collider, true);
                }
            }
            ghost.ccol.enabled = false;
            ghost.gameObject.SetActive(false);
        }

        public void ChangeGhostOpacity()
        {
            Color ghostColor = ghost.sprite.material.color;
            ghostColor.a = ghostOpacity;
            ghost.sprite.material.color = ghostColor;
        }

        public void WriteSplits()
        {
            string ilName = il.ToString();
            string path = "BepInEx/splits/" + ilName + ".txt";
            var splitStrings = new List<string>();
            foreach (var split in splitGroups[(int)il].splits)
            {
                splitStrings.Add(split.ToString());
            }
            using (var writer = new StreamWriter(path, false))
            {
                writer.WriteLine(splitGroups[(int)il].offset.TotalSeconds);
                writer.WriteLine(string.Join("\n", splitStrings.ToArray()));
                writer.WriteLine(splitGroups[(int)il].attemptsCount);
            }
        }

        void ReadSplits()
        {
            foreach (IL il in Enum.GetValues(typeof(IL)))
            {
                if (il == IL.None)
                    continue;
                string ilName = il.ToString();

                var typeIL = Type.GetType("SpeedrunPractice.Extensions.ILData." + ilName, false);

                if (typeIL == null)
                    continue;

                var splitGroup = Activator.CreateInstance(typeIL, new object[] { new List<Split>()});
                splitGroups[(int)il] = (SplitGroup)splitGroup;

                string path = "BepInEx/splits/" + ilName + ".txt";

                splitGroups[(int)il].SetSplits();
                if (File.Exists(path))
                {
                    var splitStrings = new List<string>(File.ReadAllLines(path));

                    if(int.TryParse(splitStrings[splitStrings.Count - 1], out int attempts))
                        splitGroups[(int)il].attemptsCount = attempts;

                    for (int i = 1; i != splitStrings.Count; i++)
                    {
                        if (splitStrings[i].Contains(","))
                        {
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US"); //for the format
                            string[] data = splitStrings[i].Split(',');
                            splitGroups[(int)il].splits[i - 1].SetTimes(TimeSpan.Parse(data[2]), TimeSpan.Parse(data[3]), TimeSpan.Parse(data[1]));
                        }
                    }
                }
            }
        }

        public void WriteGhostRecordings()
        {
            string ilName = il.ToString();
            string path = "BepInEx/ghostRecordings/" + ilName + ".txt";
            var ghostStrings = new List<string>();
            foreach (var record in pbGhosts)
            {
                ghostStrings.Add(record.ToString());
            }
            using (var writer = new StreamWriter(path, false))
            {
                writer.WriteLine(string.Join("\n", ghostStrings.ToArray()));
            }
        }

        void LoadGhost()
        {
            string ilName = il.ToString();
            string path = "BepInEx/ghostRecordings/" + ilName + ".txt";
            if (File.Exists(path))
            { 
                var ghostStrings = new List<string>(File.ReadAllLines(path));
                pbGhosts = new GhostRecorder[ghostStrings.Count];
                for (int i = 0; i != ghostStrings.Count; i++)
                {
                    string[] data = ghostStrings[i].Split('|');
                    string position = data[0].Substring(1, data[0].Length - 2);
                    var vectorArray = position.Split(',');

                    Vector3 pos = new Vector3(float.Parse(vectorArray[0]), float.Parse(vectorArray[1]), float.Parse(vectorArray[2]));
                    MainManager.Maps map = (MainManager.Maps)Enum.Parse(typeof(MainManager.Maps), data[4]);
                    pbGhosts[i] = new GhostRecorder(pos, int.Parse(data[1]), bool.Parse(data[2]), int.Parse(data[3]), map);
                }
            }
        }

        public void RecordGhostPb()
        {
            pbGhosts = recordings.ToArray();
        }

        public void UndoSplit()
        {
            if (splitIndex == 0)
                return;
            if (!start)
            {
                start = true;
                Console.WriteLine("set start to true in undosplit");
            }
            splitIndex--;
            var split = GetCurrentSplit();
            split.UndoSplit();
            ChangeUI(split, splitGroups[(int)il].splits[splitIndex - 1]);
        }

        public Split GetCurrentSplit() => splitGroups[(int)il].splits[splitIndex];

        DynamicFont ChangeDynamicFontColor(DynamicFont original, Color color, string text)
        {
            Vector3 position = new Vector3(original.startpos.x, original.startpos.y, original.startpos.z);
            Vector3 size = new Vector2(original.size.x, original.size.y);
            bool dropShadow = original.dropshadow;
            float frequency = original.updatefrequency;

            Destroy(original.gameObject);
            original = DynamicFont.SetUp(text, false, true, frequency, 2, 100, size, timerUI.transform,position, color, null);
            original.dropshadow = dropShadow;
            return original;
        }
     
        public IEnumerator WaitForEndOfBattle(int[] enemies)
        {
            yield return new WaitUntil(() => !MainManager.instance.inbattle);
            if (start)
                CheckBattleSplit(enemies, false);
            else
                CheckStart(enemies: enemies);
        }

        public IEnumerator WaitForEndFade()
        {
            yield return new WaitUntil(() => MainManager.musiccoroutine == null);
            EndWaspKingIL();
        }

        public void EndWaspKingIL()
        {
            var split = GetCurrentSplit();

            if (splitIndex == splitGroups[(int)il].splits.Count - 1)
            {
                split.EndSplit(TimeSpan.FromSeconds(timer));
                ChangeUI(split, split);
                start = false;
                splitGroups[(int)il].state = SplitState.Ended;
                Console.WriteLine("set start to false in endWaspKingIL");
            }
        }

        public void ExitILMode()
        {
            timerUI.gameObject.SetActive(false);
            il = IL.None;
            start = false;
            Console.WriteLine("set start to false in undosplit");
            MainManager_Ext.ilMode = false;
        }
    }
}
