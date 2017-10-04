using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;
	float zoomAim = 0;
	float onGround = 0;
	public float onGroundIn = 0.5f;
	public float onGroundOut = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //get the player's current position
        Vector3 newPos = player.transform.position;
        Vector3 posDif = newPos - transform.position;

		zoomAim = player.GetComponent<Rigidbody2D> ().velocity.magnitude;

		if (player.GetComponent<PlayerController> ().isOnGround) {
			if (onGround > 0) {
				onGround -= Time.deltaTime * onGroundIn;
			} else {
				onGround = 0;
			}
		} else {
			if (onGround < 1) {
				onGround += Time.deltaTime * onGroundOut;
			} else {
				onGround = 1;
			}
		}

		zoomAim *= onGround;
		Camera.main.orthographicSize += (zoomAim * Time.deltaTime) - (10 * Time.deltaTime);

		if (Camera.main.orthographicSize < 8) {
			Camera.main.orthographicSize = 8;
		}

		if (Camera.main.orthographicSize > 15) {
			Camera.main.orthographicSize = 15;
		}

        posDif.z = 0.0f;
        transform.Translate(posDif);
	}
}
