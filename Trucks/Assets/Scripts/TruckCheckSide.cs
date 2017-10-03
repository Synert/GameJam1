﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckCheckSide : MonoBehaviour {

    public ProceduralLevel proceduralLevel;
    public int side;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Truck")
        {
            proceduralLevel.UpdateTruckCount(side, 1);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Truck")
        {
            proceduralLevel.UpdateTruckCount(side, -1);
        }
    }
}
