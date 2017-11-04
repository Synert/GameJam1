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
        GameObject newTruck = Instantiate(Trucks[truckArray], truckPosition, Quaternion.identity);
        if(newTruck.name == "TruckB(Clone)")
        {
            newTruck.transform.Translate(new Vector3(0.0f, 0.0f, 2.0f));
        }
    }



    //GameObject.FindGameObjectsWithTag("Background");
    //Need to change Z axis of spawning bkacground trucks to 1.

    void Update ()
    {
       
    }
}
