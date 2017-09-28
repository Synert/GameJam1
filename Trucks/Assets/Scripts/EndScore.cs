using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    public ScoreTimer scoreTimer;

    Text score;

    void Start ()
    {
        score.text = "Your Final Score is: " + scoreTimer.scoreTime;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
