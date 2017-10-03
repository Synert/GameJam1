using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnScript : MonoBehaviour {

    GameObject player;

    float despawnTimer;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        float distance = player.transform.position.x - transform.position.x;

        //anything over 200 in front of you is probably a runaway truck that's gone off the terrain
        if(distance < -50.0f || distance > 200.0f)
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer > 5.0f)
            {
                Destroy(gameObject);
            }
        }

	}
}
