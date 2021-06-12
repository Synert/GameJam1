using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;
	float zoomAim = 0;
	float onGround = 0.75f;
	public float onGroundIn = 0.5f;
	public float onGroundOut = 1;
    Vector3 oldPos;

    float curSize = 8.0f;

	// Use this for initialization
	void Start () {
        oldPos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //get the player's current position
        Vector3 newPos = player.transform.position;
        Vector3 posDif = newPos - oldPos;

		zoomAim = (posDif.magnitude * 45.0f);

        if(Time.timeScale > 0)
        {
            zoomAim = zoomAim / Time.timeScale;
        }

        oldPos = newPos;

		/*if (player.GetComponent<PlayerController> ().isOnGround) {
			if (onGround > 0.9f) {
				onGround -= Time.deltaTime * onGroundIn;
			} else {
				onGround = 0.9f;
			}
		} else {
			if (onGround < 1f) {
				onGround += Time.deltaTime * onGroundOut;
			} else {
				onGround = 1f;
			}
		}*/

		//zoomAim *= onGround;

        //Debug.Log(curSize);
        //Camera.main.orthographicSize += (zoomAim * Time.deltaTime) - (5 * Time.deltaTime);

        float dif = (zoomAim - curSize) * Time.deltaTime;
        //Debug.Log(dif);

        curSize += dif;
        Camera.main.orthographicSize += (curSize - Camera.main.orthographicSize) * Time.deltaTime;


        if (Camera.main.orthographicSize < 8) {
			Camera.main.orthographicSize = 8;
		}

		if (Camera.main.orthographicSize > 20) {
			Camera.main.orthographicSize = 20;
		}

        posDif.z = 0.0f;

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10.0f);
        //transform.Translate(posDif);
	}
}
