using UnityEngine;
using System.Collections.Generic;
using EventSystem;

namespace behavior {
    public class HitManagement : MonoBehaviour {

        private void OnCollisionStay(Collision other) {
            if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
                return;

            Hurtful hurtful = other.transform.GetComponent<Hurtful>();
            if (hurtful)
            {
                other.GetContact(0).thisCollider.GetComponent<Life>().lose(hurtful.HurtAmount);
            }
        }
    }
}
