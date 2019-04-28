using UnityEngine;
using System.Collections.Generic;

namespace behavior {
    public class Life : MonoBehaviour {
        [SerializeField] private float life;
        [SerializeField] private float worthMultiplier = 1f;
        [SerializeField] private float maxLife;
        [SerializeField] private float timeOfUntouchability = 1;
        [SerializeField] private List<AudioClip> clips;

        private AudioSource audioSource;
        private float untouchableTimer;
        private Hurtful hurtful;

        private Animator animator;
        private static readonly int DIE = Animator.StringToHash("die");
        private AllRobotParts parentRobot;

        private void Awake() {
            animator = GetComponent<Animator>();
            if (transform.parent) {
                parentRobot = transform.parent.GetComponent<AllRobotParts>();
            }

            audioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        private void Start() {
            life = maxLife;
        }

        private void Update()
        {
            if (untouchableTimer > 0)
            {
                untouchableTimer -= Time.deltaTime;
                return;
            }

            if (hurtful) hurtful.hurtMe(this);
        }

        private void OnCollisionEnter(Collision other) {
            hurtful = other.transform.GetComponent<Hurtful>();
        }

        private void OnCollisionExit(Collision other) {
            hurtful = null;
        }

        public float getMyWorth() {
            return life * worthMultiplier;
        }

        public void lose(float hurtAmount) {
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
            life -= hurtAmount;
            untouchableTimer = timeOfUntouchability;
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