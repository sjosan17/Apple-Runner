using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

	// To follow a distance behind the player.
	void Start () {
        offset = transform.position - player.transform.position;
    }
	
	// Late Update due to camera can be last to move with player.
	void LateUpdate () {
        transform.position = player.transform.position + offset;
    }
}
