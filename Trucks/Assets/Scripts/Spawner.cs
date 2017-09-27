using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float spawnFrequency = 5.0f;
    public float spawnStart = 1.0f;
    public GameObject[] Trucks;
    public Vector3 truckPosition;


	void Start ()
    {
        truckPosition = transform.position;
        InvokeRepeating("Spawn", spawnStart, spawnFrequency);
    }
	
    void Spawn()
    {
        int truckArray = Random.Range(0, Trucks.Length);
        Instantiate(Trucks[truckArray], truckPosition, Quaternion.identity);
    }



    //GameObject.FindGameObjectsWithTag("Background");
    //Need to change Z axis of spawning bkacground trucks to 1.

    void Update ()
    {
       
    }
}
