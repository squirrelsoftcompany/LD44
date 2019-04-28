using behavior;
using UnityEngine;

public class Takeable : MonoBehaviour {
    [SerializeField] private Life lifeRobotPart;
    [SerializeField] private Life dependentOnPart;
    private Animator animator;
    private static readonly int PICKED = Animator.StringToHash("picked");
    private static readonly int WRONGLY_PICKED = Animator.StringToHash("wronglyPicked");
    private AudioSource audioSource;
    [SerializeField] private AudioClip wrongPick, pick;

    private void Awake() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (dependentOnPart != null && !dependentOnPart.gameObject.activeSelf) {
            if (audioSource != null) audioSource.clip = wrongPick;
            // cannot be picked up animation
            if (animator != null) {
                animator.SetTrigger(WRONGLY_PICKED);
            } else {
                onWronglyPickedAnimFinished();
            }
        } else {
            if (audioSource != null) audioSource.clip = pick;
            lifeRobotPart.bePickedUp();
            // Can and will be picked up
            if (animator != null) {
                animator.SetTrigger(PICKED);
            } else {
                onPickedUpAnimationFinished();
            }
        }
    }

    public void onWronglyPickedAnimFinished() {
        // TODO see if do something here
    }

    public void onPickedUpAnimationFinished() {
        lifeRobotPart.bePickedUp();
        Destroy(gameObject);
    }
}