using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;

    Vector3 oldPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //get the player's current position
        Vector3 newPos = player.transform.position;
        //and then their movement
        Vector3 posChange = newPos - oldPos;

        //move the camera to this position
        //transform.Translate(posChange / 5);

        //float xChange = -(transform.position.x / ((transform.position.x) + Screen.width / 2) - player.transform.position.x);

        //transform.position += new Vector3(xChange, player.transform.position.y, player.transform.position.z) * Time.deltaTime;

        Vector3 posDif = newPos - transform.position;

        transform.Translate(posDif);

        oldPos = newPos;
	}
}
