using HarmonyLib;
using UnityEngine;
using InputIOManager;
using System;

namespace SpeedrunPractice.Extensions
{
    public class BattleControl_Ext : MonoBehaviour
    {
        public static int currentActionID = -1;
        public void PracticeFKeys()
        {
            var battleControl = gameObject.GetComponent<BattleControl>();

            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.ResetIL]) && !MainManager.instance.pause && MainManager_Ext.ilMode)
            {
                MainManager.instance.GetComponent<ILTimer>().ResetIL();
            }

            if (Input.GetKeyDown(InputIO.keys[(int)PracticeKeys.FreeCam]))
            {
                MainManager_Ext.toggleFreeCam = !MainManager_Ext.toggleFreeCam;
                MainManager.PlaySound("Scroll", -1);

                if (MainManager_Ext.toggleFreeCam)
                    Cursor.lockState = CursorLockMode.Confined;
                else
                    Cursor.lockState = CursorLockMode.None;
            }
            if (Input.GetKey(InputIO.keys[(int)PracticeKeys.KillEnemies]))
            {
                for (int i = 0; i < battleControl.enemydata.Length; i++)
                {
                    battleControl.enemydata[i].hp = 0;
                    battleControl.enemydata[i].battleentity.dead = false;
                    battleControl.StartCoroutine(battleControl.CheckDead());
                }
            }
            if (battleControl.canflee && Input.GetKey(InputIO.keys[(int)PracticeKeys.FleeBattle]))
            {
                StartCoroutine(battleControl.ReturnToOverworld(true));
            }
            if (Input.GetKey(InputIO.keys[(int)PracticeKeys.ReloadBattle]))
            {
                var actiontextRef = AccessTools.FieldRefAccess<BattleControl, SpriteRenderer>("actiontext");
                var hexpcounterRef = AccessTools.FieldRefAccess<BattleControl, Transform>("hexpcounter");
                var smallexporbsRef = AccessTools.FieldRefAccess<BattleControl, Transform[]>("smallexporbs");
                var bigexporbsRef = AccessTools.FieldRefAccess<BattleControl, Transform[]>("bigexporbs");
                var cursorRef = AccessTools.FieldRefAccess<BattleControl, Transform>("cursor");

                var RefreshEnemyHPRef = AccessTools.Method(typeof(BattleControl), "RefreshEnemyHP");
                var SetFlagsRef = AccessTools.Method(typeof(BattleControl), "SetFlags");
                var RetryRef = AccessTools.Method(typeof(BattleControl), "Retry");

                if (actiontextRef(battleControl) != null)
                {
                    Destroy(actiontextRef(battleControl).gameObject);
                }
                if (hexpcounterRef(battleControl) != null)
                {
                    Destroy(hexpcounterRef(battleControl).gameObject);
                }
                if (smallexporbsRef(battleControl) != null && smallexporbsRef(battleControl).Length != 0)
                {
                    for (int j = 0; j < smallexporbsRef(battleControl).Length; j++)
                    {
                        if (smallexporbsRef(battleControl)[j] != null)
                        {
                            Destroy(smallexporbsRef(battleControl)[j].gameObject);
                        }
                    }
                }
                if (bigexporbsRef(battleControl) != null && bigexporbsRef(battleControl).Length != 0)
                {
                    for (int k = 0; k < bigexporbsRef(battleControl).Length; k++)
                    {
                        if (bigexporbsRef(battleControl)[k] != null)
                        {
                            Destroy(bigexporbsRef(battleControl)[k].gameObject);
                        }
                    }
                }
                if (cursorRef(battleControl) != null)
                {
                    Destroy(cursorRef(battleControl).gameObject);
                }
                battleControl.cancelupdate = true;
                battleControl.currentturn = battleControl.partypointer[0];
                battleControl.alreadyending = true;
                RefreshEnemyHPRef.Invoke(battleControl, null);
                SetFlagsRef.Invoke(battleControl, null);
                RetryRef.Invoke(battleControl, new object[] { false });
            }
        }

        public static float CheckMinMax(float min, float max, float expectedMin, float expectedMax, float originalChance,float newChance)
        {
            if (min == expectedMin && max == expectedMax)
            {
                return newChance;
            }
            return originalChance;
        }

        public static float CheckRNG(float min, float max, float originalChance)
        {
            if (MainManager.basicload)
            {
                var battle = MainManager.battle;
                if (MainManager.battle != null && MainManager.battle.enemy && MainManager_Ext.togglePerfectRNG)
                {
                    if (battle.enemydata != null && currentActionID >=0 && currentActionID < battle.enemydata.Length)
                    {
                        var animID = battle.enemydata[currentActionID].animid;
                        int maxAriaAttacks = 7;
                        int ariaAttack = 2; //vine

                        int minAstoHits = 2;
                        int maxAstoHits = 4;

                        float minScorpTailWait = 0.65f;
                        float maxScorpTailWait = 0.85f;

                        int b33Attack = 0; //laser
                        var hpPercentRef = AccessTools.Method(typeof(BattleControl), "HPPercent", new Type[] { typeof(MainManager.BattleData) });
                        float hpp = (float)hpPercentRef.Invoke(battle, new object[] { battle.enemydata[0] });
                        int maxB33Attacks = hpp < 0.5f ? 6 : 4;

                        var position = battle.enemydata[currentActionID].position;
                        int veguAttack = position == BattleControl.BattlePosition.Ground ? 0 : 2; //slam or bomb
                        int maxVeguAttack = position == BattleControl.BattlePosition.Ground ? 7 : 3;
                        float minVeguSlamWait = 0.7f;
                        float maxVeguSlamWait = 0.9f;

                        int maxScarletAttacks = hpp < 0.45f ? 13 : 9;
                        bool attackUp = MainManager.HasCondition(MainManager.BattleCondition.AttackUp, battle.enemydata[currentActionID]) != -1;
                        int scarletAttack = !attackUp ? 6 : 3; //attack up or beam attack

                        switch (animID)
                        {
                            case (int)MainManager.Enemies.Acolyte:
                                return CheckMinMax(min, max, 0, maxAriaAttacks, originalChance, ariaAttack);
                            case (int)MainManager.Enemies.Scarlet:
                                return CheckMinMax(min, max, 0, maxScarletAttacks, originalChance, scarletAttack);
                            case (int)MainManager.Enemies.BanditLeader:
                                return CheckMinMax(min, max, minAstoHits, maxAstoHits, originalChance, maxAstoHits-1);
                            case (int)MainManager.Enemies.Scorpion:
                                return CheckMinMax(min, max, minScorpTailWait, maxScorpTailWait, originalChance, minScorpTailWait);
                            case (int)MainManager.Enemies.BeeBoss:
                                return CheckMinMax(min, max, 0, maxB33Attacks, originalChance, b33Attack);
                            case (int)MainManager.Enemies.VenusBoss:
                                originalChance = CheckMinMax(min, max, 0, maxVeguAttack, originalChance, veguAttack);
                                originalChance = CheckMinMax(min, max, 0, 100, originalChance, 99); //def up attack up roll
                                originalChance = CheckMinMax(min, max, minVeguSlamWait, maxVeguSlamWait, originalChance, minVeguSlamWait);
                                break;
                            case (int)MainManager.Enemies.PrimalWeevil:
                                return CheckMinMax(min, max, 0, 100, originalChance, 99); //attack up roll
                            case (int)MainManager.Enemies.KeyL:
                            case (int)MainManager.Enemies.KeyR:
                                return CheckMinMax(min, max, 0, 100, originalChance, 0); //spin attack
                        }

                        if (animID == (int)MainManager.Enemies.EverlastingKing && position == BattleControl.BattlePosition.Ground)
                        {
                            return CheckMinMax(min, max, 0, 100, originalChance, 99); //vine attack
                        }
                    }
                }
            }
            return originalChance;
        }

        public static int CheckAttacks(int originalAttack)
        {
            var battle = MainManager.battle;
            if(MainManager.battle.enemy && MainManager_Ext.togglePerfectRNG)
            {
                if (battle.enemydata != null && currentActionID >= 0 && currentActionID < battle.enemydata.Length)
                {
                    var animID = battle.enemydata[currentActionID].animid;
                    switch (animID)
                    {
                        case (int)MainManager.Enemies.BanditLeader:
                            return 0; //multihits
                        case (int)MainManager.Enemies.Scorpion:
                            return 1; //tailattack
                        case (int)MainManager.Enemies.Centipede:
                            return 1; //hornattack
                        case (int)MainManager.Enemies.Ahoneynation:
                            return 2; //charge
                        case (int)MainManager.Enemies.PrimalWeevil:
                            return 0; //roar
                        case (int)MainManager.Enemies.UltimaxTank:
                            return originalAttack <= 3 ? 2 : originalAttack; //charge
                        case (int)MainManager.Enemies.DeadLanderG:
                            return 2; //laser attack
                        case (int)MainManager.Enemies.WaspKing:
                            return 1; //axe throw
                        case (int)MainManager.Enemies.EverlastingKing:
                            return originalAttack <= 5 ? 0 : originalAttack; //vine attack
                    }
                }
            }
            return originalAttack;
        }

        public static bool CheckTarget()
        {
            var getSingleTargetRef = AccessTools.Method(typeof(BattleControl), "GetSingleTarget", new Type[] { typeof(int)});
            var enemyInFieldRef = AccessTools.Method(typeof(BattleControl), "EnemyInField", new Type[] { typeof(int[]) });

            //always focuses kabbu
            var enemies = new int[] { (int)MainManager.Enemies.BanditLeader, (int)MainManager.Enemies.EverlastingKing };
            var enemyInField = (int)enemyInFieldRef.Invoke(MainManager.battle, new object[] { enemies });
            if (MainManager.instance.playerdata[1].hp != 0 && enemyInField != -1)
            {
                getSingleTargetRef.Invoke(MainManager.battle, new object[] { 1 });
                return false;
            }

            //always focuses vi
            enemies = new int[] { (int)MainManager.Enemies.Centipede, (int)MainManager.Enemies.Scorpion, (int)MainManager.Enemies.WaspKing, (int)MainManager.Enemies.WaspGeneral };
            enemyInField = (int)enemyInFieldRef.Invoke(MainManager.battle, new object[] { enemies });
            if (MainManager.instance.playerdata[0].hp != 0 && enemyInField != -1)
            {
                getSingleTargetRef.Invoke(MainManager.battle, new object[] { 0 });
                return false;
            }
            return true;
        }
    }
}
