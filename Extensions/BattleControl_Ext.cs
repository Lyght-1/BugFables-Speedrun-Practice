﻿using HarmonyLib;
using UnityEngine;

namespace SpeedrunPractice.Extensions
{
  public class BattleControl_Ext : MonoBehaviour
  {
    void KillEnemy(ref MainManager.BattleData enemy, BattleControl battleControl)
    {
	  enemy.hp = 0;
	  enemy.battleentity.dead = false;
	  StartCoroutine(battleControl.CheckDead());
	}
		public void PracticeFKeys()
		{
			var battleControl = this.gameObject.GetComponent<BattleControl>();
			if (Input.GetKey(KeyCode.F3))
			{
			  var enemyInFieldRef = AccessTools.Method(typeof(BattleControl), "EnemyInField", new System.Type[] { typeof(int)});
			  int indexWeb = (int)enemyInFieldRef.Invoke(battleControl, new object[] { (int)MainManager.Enemies.MothWeb });
			  if (indexWeb != -1)
			  {
			    KillEnemy(ref battleControl.enemydata[indexWeb], battleControl);
			  } 
			  else
			  {
			    for (int i = 0; i < battleControl.enemydata.Length; i++)
				  KillEnemy(ref battleControl.enemydata[i], battleControl);
			  }
			}
			if (battleControl.canflee && Input.GetKey(KeyCode.F4))
			{
				base.StartCoroutine(battleControl.ReturnToOverworld(true));
			}
			if (Input.GetKey(KeyCode.F5))
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
	}
}
