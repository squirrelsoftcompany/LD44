using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class MoveDummy : MonoBehaviour {
    [SerializeField] private GameEvent pickupObj;

    private float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start() { }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name.Equals("Wall")) {
            pickupObj.Raise();
        }
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, 0f,
            Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
    }
}