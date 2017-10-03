using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckCheck : MonoBehaviour
{

    public GameObject player;
    Vector3 oldPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //get the player's current position
        Vector3 newPos = player.transform.position;
        Vector3 posDif = newPos - transform.position;

        posDif.z = 0.0f;
        transform.Translate(posDif);

        oldPos = newPos;
    }

}
