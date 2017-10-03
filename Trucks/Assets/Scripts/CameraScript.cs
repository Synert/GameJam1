using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;
	public Vector2 vel;
	public float velChange;
	
	// Update is called once per frame
	void Update () {

        //move the camera to this position
        //transform.Translate(posChange / 5);

		vel.x = player.transform.position.x - transform.position.x;
		vel.y = player.transform.position.y - transform.position.y;

		vel *= velChange;

		GetComponent<Rigidbody2D> ().velocity = vel;
	}
}
