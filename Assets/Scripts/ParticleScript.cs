using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {
    // Used for the pickup bonus points that the player can collect.
    private ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!ps.isPlaying)
        {
            Destroy(gameObject); // Avoids clogging up the game with GameObjects.
        }
	}
}
