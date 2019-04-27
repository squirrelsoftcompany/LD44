using UnityEngine;

public class Hurtful : MonoBehaviour {
    [SerializeField] private float hurtAmount;

    public void hurtMe(Life life) {
        life.lose(hurtAmount);
    }
}