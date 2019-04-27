using behavior;
using UnityEngine;

public class Takeable : MonoBehaviour {
    [SerializeField] private Life lifeRobotPart;
    private Animator animator;
    private static readonly int PICKED = Animator.StringToHash("picked");

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (animator != null) {
            animator.SetTrigger(PICKED);
        } else {
            onPickedUpAnimationFinished();
        }
    }

    public void onPickedUpAnimationFinished() {
        lifeRobotPart.bePickedUp();
        Destroy(gameObject);
    }
}