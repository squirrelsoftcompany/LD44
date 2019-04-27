using UnityEngine;

namespace behavior {
    public class Life : MonoBehaviour {
        [SerializeField] private float life;
        [SerializeField] private float worthMultiplier = 1f;
        [SerializeField] private float maxLife;

        private Animator animator;
        private static readonly int DIE = Animator.StringToHash("die");
        private AllRobotParts parentRobot;
    
        private void Awake() {
            animator = GetComponent<Animator>();
            if (transform.parent) {
                parentRobot = transform.parent.GetComponent<AllRobotParts>();
            }
        }

        // Start is called before the first frame update
        private void Start() {
            life = maxLife;
        }

        private void OnCollisionEnter(Collision other) {
            var hurtful = other.transform.GetComponent<Hurtful>();
            if (hurtful != null) hurtful.hurtMe(this);
        }

        public float getMyWorth() {
            return life * worthMultiplier;
        }

        public void lose(float hurtAmount) {
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
                gameObject.SetActive(false);
                GetComponent<AllRobotParts>().die();
            }
        }
    }
}