using System;
using behavior;
using UnityEngine;

public class Hurtful : MonoBehaviour {
    [SerializeField] private float hurtAmount;
    private const int maxParticules = 10;

    public void hurtMe(Life life) {
        life.lose(hurtAmount);
    }

    public void hurtMe(Life life, int nbParticules) {
        var part = Math.Min(nbParticules, maxParticules);
        life.lose(hurtAmount * part);
    }
}