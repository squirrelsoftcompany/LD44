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
                List<ContactPoint> contacts = new List<ContactPoint>();
                other.GetContacts(contacts);
                foreach(ContactPoint contact in contacts)
                {
                    string str = "other: " + contact.otherCollider.name
                            + "\n - Parent: " + contact.otherCollider.transform.parent.name
                            + "\nthis: " + contact.thisCollider.name
                            + "\n - Parent: " + contact.thisCollider.transform.parent.name;
                    Debug.Log(str);
                }

                other.GetContact(0).thisCollider.GetComponent<Life>().lose(hurtful.HurtAmount);
            }
        }
    }
}