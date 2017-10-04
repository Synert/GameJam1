using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerDetector : MonoBehaviour {

	public bool forceGrab = false;

	// Use this for initialization
	void Start () {
		if (forceGrab) {
			MultiplayerManager[] temp = GameObject.FindObjectsOfType<MultiplayerManager> ();
			foreach (MultiplayerManager t in temp) {
				t.grab ();
			}
		}
	}
}