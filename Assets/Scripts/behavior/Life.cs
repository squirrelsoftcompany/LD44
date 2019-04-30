using UnityEngine;
using System.Collections.Generic;
using EventSystem;

namespace behavior {
    public class Life : MonoBehaviour {
        [SerializeField] private float life;
        [SerializeField] private float worthMultiplier = 1f;
        [SerializeField] private float maxLife;
        [SerializeField] private List<AudioClip> clips;
        [SerializeField] private GameEvent changeLife;

        private AudioSource audioSource;

        public Animator animator { private get; set; }
        private static readonly int DIE = Animator.StringToHash("die");
        public AllRobotParts parentRobot { get; set; }
        private static readonly int WRONGLY_PICKED = Animator.StringToHash("wronglyPicked");

        public Touchability touchability { private get; set; }

        private void Awake() {
//            if (transform.parent) {
//                parentRobot = transform.parent.GetComponent<AllRobotParts>();
//            }

            audioSource = GetComponent<AudioSource>();
//            touchable = GetComponent<Touchability>();
//            if (! touchable)
//            {
//                touchable = transform.parent.GetComponent<Touchability>();
//            }
        }

        // Start is called before the first frame update
        private void Start() {
            life = maxLife;
            changeLife.Raise();
        }

        public float getMyWorth() {
            return life * worthMultiplier;
        }

        public void lose(float hurtAmount) {
            if (!touchability.Touchable) return;

            audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
            touchability.Hitted();

            life -= hurtAmount;
            if (life <= 0) {
                die();
            }

            changeLife.Raise();
        }

        public void die() {
            // die a magnificent death, then be destroyed
            if (animator != null) {
                animator.SetTrigger(DIE);
                Invoke(nameof(onDieAnimationFinished), 1f);
            } else {
                onDieAnimationFinished();
            }
        }


        private void onDieAnimationFinished() {
            if (parentRobot != null) {
                parentRobot.removePart(this);
            } else {
                // We are the parent, and we should die
                GetComponent<AllRobotParts>().die();
                gameObject.SetActive(false);
            }
        }

        public void bePickedUp() {
            if (parentRobot) {
                parentRobot.addPart(this);
            }
        }
    }
}