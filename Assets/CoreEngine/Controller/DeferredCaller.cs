using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.controller {

    public class DeferredCaller : MonoBehaviour {

        private List<Action> pendingCalls;

        private void Awake() {
            pendingCalls = new List<Action>(); 
        }

        private void Update() {
            if (pendingCalls.Count > 0) {
                foreach (Action fun in pendingCalls) {
                    fun();
                }

                pendingCalls = new List<Action>();
            }
        }

        public void CallLimitedToOncePerFrame(Action fun) {
            if (pendingCalls.Contains(fun))
                return;

            pendingCalls.Add(fun);
        }

    }
}
