using EventSystem;
using UnityEngine;

public class ArchEnd : MonoBehaviour {
    [SerializeField] private GameEvent dieEvent;

    [SerializeField] private Animator animatorDoor;
    private static readonly int OPEN = Animator.StringToHash("open");

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        // We won!
        dieEvent.sentBool = true;
        dieEvent.Raise();
    }

    public void openDoor() {
        animatorDoor.SetTrigger(OPEN);
    }
}