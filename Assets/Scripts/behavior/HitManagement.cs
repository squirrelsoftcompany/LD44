using UnityEngine;
using System.Collections.Generic;
using EventSystem;

namespace behavior {
    public class HitManagement : MonoBehaviour {

        private void OnCollisionStay(Collision other) {
            if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
                return;

            Hurtful hurtful = other.transform.GetComponent<Hurtful>();
            Life part = other.GetContact(0).thisCollider.GetComponent<Life>();
            if (hurtful && part)
            {
                part.lose(hurtful.HurtAmount);
            }
        }

        private void Update()
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}
