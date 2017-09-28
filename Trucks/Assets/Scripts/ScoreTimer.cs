using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTimer : MonoBehaviour {

    public float scoreTime = 10000f;
    Text score;
	
	void Start ()
    {
        score = GetComponent<Text> ();
    }
	
	
	void Update ()
    {

        scoreTime -= ((Time.unscaledDeltaTime) *  50);
        score.text = "Score: " + (int)scoreTime;
	}
}
