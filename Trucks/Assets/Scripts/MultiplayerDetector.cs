using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerDetector : MonoBehaviour {

	public bool forceGrab = false;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		if (forceGrab) {
			MultiplayerManager[] temp = GameObject.FindObjectsOfType<MultiplayerManager> ();
			foreach (MultiplayerManager t in temp) {
				t.grab ();
			}
		}
	}
}