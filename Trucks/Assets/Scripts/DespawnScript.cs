using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnScript : MonoBehaviour {

    GameObject player;
    GameObject m_child;
    float despawnTimer;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (tag == "Truck")
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "Truck")
                {
                    m_child = child.gameObject;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        float distance = 0.0f;
        float y_distance = 0.0f;

        if (tag == "Truck")
        {
            distance = m_child.transform.position.x - player.transform.position.x;
            y_distance = Mathf.Abs(m_child.transform.position.y - player.transform.position.y);
        }
        else
        {
            distance = transform.position.x - player.transform.position.x;
        }

        //anything over 200 in front of you is probably a runaway truck that's gone off the terrain
        if(distance < -50.0f || distance > 250.0f || y_distance > 250.0f)
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer > 2.0f)
            {
                Destroy(gameObject);
            }
        }

	}
}
