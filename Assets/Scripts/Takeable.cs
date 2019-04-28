using behavior;
using EventSystem;
using UnityEngine;

public class Takeable : MonoBehaviour {
    [SerializeField] private Life dependentOnPart;
    private Animator animator;
    private static readonly int PICKED = Animator.StringToHash("picked");
    private static readonly int WRONGLY_PICKED = Animator.StringToHash("wronglyPicked");
    private AudioSource audioSource;
    [SerializeField] private AudioClip wrongPick, pick;
    [SerializeField] private GameEvent pickedPart;

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
            pickedPart.Raise();
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
        Destroy(gameObject);
    }
}