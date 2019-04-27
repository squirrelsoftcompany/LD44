using System.Collections.Generic;
using UnityEngine;

namespace EventSystem {
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public string sentString;
        public int sentInt;
        public float sentFloat;
        public bool sentBool;

        public void Raise() {
            for (var i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(this);
            }
        }

        public void RegisterListener(GameEventListener listener) {
            if (!listeners.Contains(listener)) {
                listeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener) {
            if (listeners.Contains(listener)) {
                listeners.Remove(listener);
            }
        }
    }
}