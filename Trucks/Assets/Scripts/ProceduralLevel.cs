using System.Collections;
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

    public GameObject player;

    float currentHeight;
    float currentX;
    float previousX;
    double difference;

    float floor_width;
    float start_x;
    int floors_spawned;

    //time to continue using same parameters in ms
    float modeTime;
    int mode;
    float upperBound;
    float lowerBound;

    float heightDiff;
    float prevDiff;

    // Use this for initialization
    void Start () {
        currentHeight = 0.0f;
        previousX = 0.0f;
        difference = 50.0f;
        start_x = -20.0f;
        floor_width = floor.GetComponent<Renderer>().bounds.size.x;
        floors_spawned = 0;
        modeTime = 1000.0f;
        upperBound = 0.0f;
        lowerBound = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        currentX = player.transform.position.x;
        //distance moved since we last placed a floor prefab
        difference += Mathf.Abs(currentX - previousX);
        previousX = currentX;

        while(difference >= floor_width)
        {
            float prevHeight = currentHeight;
            float testHeight = currentHeight;
            testHeight += Random.Range(lowerBound, upperBound);

            heightDiff = testHeight - prevHeight;

            int attempt = 0;

            while(Mathf.Abs(heightDiff - prevDiff) > 0.4f && mode != 6)
            {
                testHeight = currentHeight;
                testHeight += Random.Range(lowerBound, upperBound);

                heightDiff = testHeight - prevHeight;
                attempt++;

                if(attempt > 5)
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

            if(mode == 6)
            {
                heightDiff = 0.0f;
            }

            currentHeight = testHeight;

            prevDiff = heightDiff;

            floors_spawned++;
            difference -= floor_width;
            GameObject new_floor = (GameObject)Instantiate(floor);
            new_floor.transform.Translate(start_x + (floor_width * floors_spawned), currentHeight, 0.0f);
            new_floor.transform.Rotate(0.0f, 0.0f, heightDiff * 30);
            new_floor.transform.localScale += new Vector3(Mathf.Abs(heightDiff), 0.0f, 0.0f);
        }

        modeTime -= 1000.0f * Time.deltaTime;
        if(modeTime <= 0.0f)
        {
            modeTime = Random.Range(1.0f, 5.0f) * 1000;
            int newMode = Random.Range(0, 7);

            if(mode == 6)
            {
                heightDiff = 0.0f;
                prevDiff = 0.0f;
            }
            switch(newMode)
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
                    //random uphill
                    upperBound = Random.Range(0.2f, 0.6f);
                    lowerBound = Random.Range(-0.1f, 0.2f);
                    break;
                case 5:
                    //solid uphill
                    upperBound = 0.9f;
                    lowerBound = 0.6f;
                    break;
                case 6:
                    //jump down
                    upperBound = -1.6f * 3;
                    lowerBound = -1.6f * 3;
                    modeTime = 100.0f;
                    break;
                default:
                    upperBound = 0.0f;
                    lowerBound = 0.0f;
                    break;
            }
            mode = newMode;
        }
    }
}
