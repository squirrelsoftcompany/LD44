using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace behavior {
    public class AllRobotParts : MonoBehaviour {
        [SerializeField] private List<Life> myParts;

        public float getMyWorth() {
            return myParts.Where(life => life.gameObject.activeSelf)
                .Sum(lifePart => lifePart.getMyWorth());
        }

        public void removePart(Life life) {
            life.gameObject.SetActive(false);
        }

        public void addPart(Life part) {
            part.gameObject.SetActive(transform);
        }
    }
}