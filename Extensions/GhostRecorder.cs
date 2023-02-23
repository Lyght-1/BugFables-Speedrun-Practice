using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpeedrunPractice.Extensions
{
    public class GhostRecorder : MonoBehaviour
    {
        public Vector3 position;
        public int animState;
        public bool flip;
        public int animID;
        public MainManager.Maps currentMap;
        public GhostRecorder(Vector3 pos, int anim, bool flip, int animID, MainManager.Maps currentMap)
        {
            position = pos;
            animState = anim;
            this.flip = flip;
            this.animID = animID;
            this.currentMap = currentMap;
        }

        public override string ToString()
        {
            return position.ToString() + "|" + animState + "|" + flip + "|" + animID + "|" +currentMap.ToString();
        }
    }
}
