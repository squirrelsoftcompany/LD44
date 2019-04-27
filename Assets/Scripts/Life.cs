using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {
    [SerializeField] private float life;

    [SerializeField] private float maxLife;

    // Start is called before the first frame update
    private void Start() {
        life = maxLife;
    }

    private void OnCollisionEnter(Collision other) {
        var hurtful = other.transform.GetComponent<Hurtful>();
        if (hurtful != null) hurtful.hurtMe(this);
    }

    // Update is called once per frame
    void Update() { }

    public void lose(float hurtAmount) {
        life -= hurtAmount;
    }
}