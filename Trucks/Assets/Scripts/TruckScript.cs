using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckScript : MonoBehaviour
{

    WheelJoint2D[] wheels;

    JointMotor2D newMotor;
    string[] trailerText = { "HEAVY LOAD", "FRAGILE", "RATE MY DRIVING", "GRIZZLED GAMES", "IS THAT A PLANE", "SURPRISE",
    "FIRST CLASS", "", "DO NOT FLIP" };

    public float startSpeed = 2500.0f;
    public float acc = 10.0f;
    float currentSpeed;

    // Use this for initialization
    void Start()
    {
        wheels = GetComponentsInChildren<WheelJoint2D>();
        GetComponentInChildren<Text>().text = trailerText[Random.Range(0, trailerText.Length)];
        currentSpeed = startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //apply the current speed
        if (currentSpeed < 15000.0f)
        {
            currentSpeed += acc * Time.deltaTime;
        }

        newMotor = wheels[0].motor;

        //needs to be negative to work!
        newMotor.motorSpeed = -currentSpeed;

        foreach (WheelJoint2D wheel in wheels)
        {
            wheel.motor = newMotor;
            wheel.useMotor = true;
            wheel.enabled = true;
        }
    }
}
