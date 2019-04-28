using UnityEngine;
using System.Collections.Generic;

namespace behavior {
    public class Life : MonoBehaviour {
        [SerializeField] private float life;
        [SerializeField] private float worthMultiplier = 1f;
        [SerializeField] private float maxLife;
        [SerializeField] private List<AudioClip> clips;

        private AudioSource audioSource;

        private Animator animator;
        private Touchability touchable;
        private static readonly int DIE = Animator.StringToHash("die");
        private AllRobotParts parentRobot;
        private static readonly int WRONGLY_PICKED = Animator.StringToHash("wronglyPicked");

        private void Awake() {
            animator = GetComponent<Animator>();
            if (transform.parent) {
                parentRobot = transform.parent.GetComponent<AllRobotParts>();
            }

            audioSource = GetComponent<AudioSource>();
            touchable = GetComponent<Touchability>();
            if (! touchable)
            {
                touchable = transform.parent.GetComponent<Touchability>();
            }
        }

        // Start is called before the first frame update
        private void Start() {
            life = maxLife;
        }

        private void OnCollisionStay(Collision other)
        {
            Hurtful hurtful = other.transform.GetComponent<Hurtful>();
            if (hurtful) lose(hurtful.HurtAmount);
        }

        public float getMyWorth() {
            return life * worthMultiplier;
        }

        public void lose(float hurtAmount) {
            if (! touchable.Touchable) return;
            
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
            touchable.Hitted();

            life -= hurtAmount;
            if (life <= 0) {
                die();
            }
        }

        public void die() {
            // die a magnificent death, then be destroyed
            if (animator != null) {
                animator.SetTrigger(DIE);
            } else {
                onDieAnimationFinished();
            }
        }

        public void onDieAnimationFinished() {
            if (parentRobot != null) {
                parentRobot.removePart(this);
            } else {
                // We are the parent, and we should die
                GetComponent<AllRobotParts>().die();
                gameObject.SetActive(false);
            }
        }

        public void bePickedUp() {
            var allRobotParts = transform.parent.GetComponent<AllRobotParts>();
            if (allRobotParts != null) {
                allRobotParts.addPart(this);
            }
        }
    }
}