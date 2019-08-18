using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenAppleParticleScript : MonoBehaviour {
    // Used for the pickup bonus points that the player can collect.
    private ParticleSystem ga;

	// Use this for initialization
	void Start () {
        ga = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!ga.isPlaying)
        {
            Destroy(gameObject); // Avoids clogging up the game with GameObjects.
        }
	}
}
