using System;
using behavior;
using UnityEngine;

public class Hurtful : MonoBehaviour {
	public float HurtAmount {
		get { return hurtAmount; }
	}

    [SerializeField] private float hurtAmount = 10;
    private const int maxParticules = 10;
}