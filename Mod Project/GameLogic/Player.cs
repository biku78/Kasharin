using UnityEngine;
using System;
using MonoMod.RuntimeDetour;
using System.Reflection;

namespace Kasharin {
    public class Player : MonoBehaviour {
        GameObject PlrPreFab;
        public Player(GameObject plr) {
            this.PlrPreFab = plr;
            
        }

        public void UpdatePosition(Vector3 position) {
            try {
            this.PlrPreFab.transform.Translate(position);
            } catch (Exception e) {
                Module.Log(e.ToString());

            }
        }
    }
}