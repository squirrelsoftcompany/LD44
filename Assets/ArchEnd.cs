using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class ArchEnd : MonoBehaviour {
    [SerializeField] private GameEvent dieEvent;

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        // We won!
        dieEvent.sentBool = true;
        dieEvent.Raise();
    }
}