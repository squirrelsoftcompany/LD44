using behavior;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hurtful : MonoBehaviour {
    [SerializeField] private float hurtAmount;

    public void hurtMe(Life life) {
        life.lose(hurtAmount);
    }
}