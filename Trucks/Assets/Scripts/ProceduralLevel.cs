﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevel : MonoBehaviour {

    //all of the objects that need to be spawned in
    //set these in the scene itself
    public GameObject floor;
    public GameObject ramp;
    //placeholder names
    public GameObject obstacle_1;
    public GameObject obstacle_2;

    public GameObject truckF;

    public GameObject player;

    float currentHeight;
    float currentX;
    float previousX;
    double difference;

    int[] trucks_on_screen = new int[3];
    float[] trucks_expected = new float[3];

    float floor_width;

    //where to spawn the floors from at first
    float start_x;

    //this is just used for the new floor offset
    int floors_spawned;

    //time to continue using same parameters in ms
    float modeTime;
    int mode = 1;
    float upperBound;
    float lowerBound;

    float heightDiff;
    float prevDiff;

    public GameObject empty;
    public Material lineMaterial;
    const int thickness = 80;
    LineRenderer[] m_line = new LineRenderer[thickness];
    int curIndex = 0;
    Vector3 prevPos;
    Vector3 prevPrevPos;

    // Use this for initialization
    void Start () {
        currentHeight = 0.0f;
        previousX = 0.0f;
		//distance from player capable of spawning
        difference = 120.0f;
        start_x = -50.0f;
        floor_width = floor.GetComponent<Renderer>().bounds.size.x;
        floors_spawned = 0;
        modeTime = 10.0f;
        upperBound = 0.0f;
        lowerBound = 0.0f;

        //truck spawning
        for (int i = 0; i < 3; i++)
        {
            trucks_expected[i] = 0.0f;
            trucks_on_screen[i] = 0;
        }

        trucks_on_screen[1] = 1;

        lineMaterial.SetTextureScale("_MainTex", new Vector2(3f, 3f));

        for(int i = 0; i < thickness; i++)
        {
            GameObject newEmpty = Instantiate(empty);
            m_line[i] = newEmpty.AddComponent<LineRenderer>();
            m_line[i].positionCount = 0;
            m_line[i].material = lineMaterial;
            Color temp = new Color(0.75f - i * 0.05f, 0.9f - i * 0.025f, 0.5f - i * 0.05f);
            m_line[i].SetColors(temp, temp);
            m_line[i].widthMultiplier = 1f;
            m_line[i].textureMode = LineTextureMode.Tile;
        }
    }
	
	// Update is called once per frame
	void Update () {
        GenerateFloor();
        GenerateTruck();
    }

    public void UpdateTruckCount(int side, int add)
    {
        trucks_on_screen[side] += add;
    }

    void GenerateTruck()
    {
        for(int i = 0; i < 3; i++)
        {
            if(trucks_expected[i] > 0.0f)
            {
                trucks_expected[i] -= Time.deltaTime;
            }
        }

        if(trucks_on_screen[0] < 2 && trucks_expected[0] < 8.0f)
        {
            //get height of floor at left
            float randPos = player.transform.position.x - 25.0f - Random.Range(0.0f, 25.0f);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(randPos, player.transform.position.y + 75.0f), -Vector2.up, 150.0f);

            float newHeight = player.transform.position.y;

            if(hit.collider != null)
            {
                newHeight = hit.point.y + 5.0f;
            }

            GameObject new_truck = (GameObject)Instantiate(truckF);
			new_truck.GetComponent<DespawnScript> ().multiplayer = true;

            new_truck.transform.position = new Vector3(randPos, newHeight);
            trucks_expected[0] += 6.5f;

            new_truck.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(Random.Range(1500.0f, 3000.0f), Random.Range(-500.0f, 250.0f)));
        }

        //center- does the player have a truck under them?
        if (trucks_on_screen[1] < 1 && trucks_expected[1] < 1.0f)
        {
			GameObject new_truck = (GameObject)Instantiate(truckF);
			new_truck.GetComponent<DespawnScript> ().multiplayer = true;

            new_truck.transform.position = new Vector3(player.transform.position.x + Random.Range(-25.0f, 25.0f), player.transform.position.y + 20.0f + Random.Range(-5.0f, 5.0f));
            trucks_expected[1] += 4.0f;

            new_truck.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(Random.Range(1000.0f, 2500.0f), -500.0f));
        }

        if (trucks_on_screen[2] < 1 && trucks_expected[2] < 5.0f)
        {
            //get height of floor at right
            float randPos = player.transform.position.x + 25.0f + Random.Range(0.0f, 25.0f);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(randPos, player.transform.position.y + 75.0f), -Vector2.up, 150.0f);

            float newHeight = player.transform.position.y;

            if (hit.collider != null)
            {
                newHeight = hit.point.y + 5.0f;
            }

            GameObject new_truck = (GameObject)Instantiate(truckF);
			new_truck.GetComponent<DespawnScript> ().multiplayer = true;

            new_truck.transform.position = new Vector3(randPos, newHeight);
            trucks_expected[2] += 4.5f;

            new_truck.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(Random.Range(-500.0f, 2500.0f), Random.Range(250.0f, -1000.0f)));
        }
    }

    void GenerateFloor()
    {
        currentX = player.transform.position.x;
        //distance moved since we last placed a floor prefab
        difference += currentX - previousX;
        previousX = currentX;

        while (difference >= floor_width)
        {
            float prevHeight = currentHeight;
            float testHeight = currentHeight;
            testHeight += Random.Range(lowerBound, upperBound) * floor.transform.lossyScale.x;

            heightDiff = testHeight - prevHeight;

            int attempt = 0;

            //makes sure the difference in slope angle isn't too severe
            //and smoothes the terrain out if it is
            while (Mathf.Abs(heightDiff - prevDiff) > 0.4f && mode != 6)
            {
                testHeight = currentHeight;
                testHeight += Random.Range(lowerBound, upperBound) * floor.transform.lossyScale.x;

                heightDiff = testHeight - prevHeight;
                attempt++;

                //prevent infinite loop and manually smooth
                if (attempt > 5)
                {
                    if (heightDiff > prevDiff)
                    {
                        testHeight -= (Mathf.Abs(heightDiff - prevDiff) - 0.3f);
                        heightDiff = testHeight - prevHeight;
                    }
                    else
                    {
                        testHeight += (Mathf.Abs(heightDiff - prevDiff) - 0.3f);
                        heightDiff = testHeight - prevHeight;
                    }
                }
            }

            if (mode == 6)
            {
                heightDiff = 0.0f;
            }

            currentHeight = testHeight;

            prevDiff = heightDiff;

            floors_spawned++;
            difference -= floor_width;
            GameObject new_floor = (GameObject)Instantiate(floor);
			new_floor.GetComponent<GameOverMenu> ().multiplayer = true;
			new_floor.GetComponent<DespawnScript> ().multiplayer = true;
            new_floor.transform.Translate(start_x + (floor_width * floors_spawned), currentHeight, 0.0f);

            Vector3 tempPos = new Vector3(new_floor.transform.position.x, new_floor.transform.position.y);

            for(int i = 0; i < thickness; i++)
            {
                m_line[i].positionCount++;
                m_line[i].SetPosition(curIndex, tempPos - new Vector3(0.0f, i * 0.35f));
            }

            curIndex++;

            new_floor.GetComponent<Renderer>().sortingOrder = -1;
            new_floor.transform.Rotate(0.0f, 0.0f, heightDiff * 33);
            if (mode == 6)
            {
                new_floor.transform.Translate(0.0f, Mathf.Abs(upperBound) / 2, 0.0f);
                new_floor.transform.localScale += new Vector3(Mathf.Abs(upperBound) / 1.6f, 0.0f, 0.0f);
                new_floor.transform.Rotate(0.0f, 0.0f, 90.0f);
            }
            new_floor.transform.localScale += new Vector3(Mathf.Abs(heightDiff), 0.0f, 0.0f);
        }

        modeTime -= 1000.0f * Time.deltaTime;

        //choose a new generation mode
        if (modeTime <= 0.0f)
        {
            modeTime = Random.Range(1.0f, 5.0f) * 1000;
            int newMode = Random.Range(2, 5);

            if (mode == 4)
            {
                heightDiff = 0.0f;
                prevDiff = 0.0f;
            }

            switch (newMode)
            {
                case 0:
                    //flat ground
                    upperBound = 0.0f;
                    lowerBound = 0.0f;
                    break;
                case 1:
                    //random bumpy
                    upperBound = Random.Range(0.1f, 0.5f);
                    lowerBound = -Random.Range(0.1f, 0.5f);
                    break;
                case 2:
                    //random downhill
                    upperBound = -Random.Range(0.0f, 0.3f);
                    lowerBound = -Random.Range(0.4f, 1.6f);
                    break;
                case 3:
                    //solid downhill
                    upperBound = -Random.Range(0.7f, 1.0f);
                    lowerBound = -Random.Range(1.0f, 1.6f);
                    break;
                case 4:
                    //jump down
                    upperBound = -Random.Range(1.6f, 8.0f);
                    lowerBound = upperBound;
                    modeTime = 100.0f;
                    break;
                default:
                    //default to flat
                    upperBound = 0.0f;
                    lowerBound = 0.0f;
                    break;
            }
            mode = newMode;
        }
    }
}
