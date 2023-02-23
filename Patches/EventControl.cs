using HarmonyLib;
using SpeedrunPractice.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedrunPractice.Patches
{
    [HarmonyPatch(typeof(EventControl), "StartEvent")]
    public class PatchEventControlStartEvent
    {
        static bool Prefix(EventControl __instance, int id)
        {
            if(id >= 500)
            {
                if (id == 506)
                {
                    MainManager.instance.GetComponent<ILTimer>().SaveGolds();
                    return false;
                }

                if (id == 507)
                {
                    MainManager.instance.GetComponent<ILTimer>().ResetSplits();
                    return false;
                }
                if(id == 505)
                {
                    IL il = (IL)MainManager.instance.flagvar[0];
                    if (il != IL.None)
                        MainManager.instance.StartCoroutine(MainManager.instance.GetComponent<ILTimer>().StartIL(il));
                    return false;
                }
            }
            

            if(id == 204 && MainManager_Ext.ilMode && MainManager.instance.GetComponent<ILTimer>().il == IL.WaspKing)
            {
                MainManager.instance.GetComponent<ILTimer>().EndWaspKingIL();
            }
            return true;
        }
    }
}
