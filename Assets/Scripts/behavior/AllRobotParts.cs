using System.Collections.Generic;
using System.Linq;
using EventSystem;
using UnityEngine;

namespace behavior {
    public class AllRobotParts : MonoBehaviour {
        [SerializeField] private List<Life> myParts;
        [SerializeField] private GameEvent dieEvent;

        private void Start() {
            var touchable = GetComponent<Touchability>();

            myParts.ForEach(part => {
                part.touchability = touchable;
                part.animator = part.transform.parent.GetComponent<Animator>();
                part.parentRobot = this;
            });
            GetComponent<Life>().touchability = touchable;
            GetComponent<Life>().animator = GetComponent<Animator>();

        }

        public float getMyWorth() {
            return myParts.Where(life => life.gameObject.activeSelf)
                .Sum(lifePart => lifePart.getMyWorth());
        }

        public void removePart(Life life) {
            life.gameObject.SetActive(false);
        }

        public void addPart(Life part) {
            part.gameObject.SetActive(true);
        }

        public void die() {
            dieEvent.Raise();
        }
    }
}