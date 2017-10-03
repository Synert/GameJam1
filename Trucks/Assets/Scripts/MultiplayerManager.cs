using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour {

	public List<GameObject> objToSpawn = new List<GameObject>();
	List<AbilityData> abilities = new List<AbilityData>();
	public float delay = 0;
	public float currentYPosition = 0;
	public bool player1 = false;
	public bool testInput = false;
	GameObject player = null;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		foreach (GameObject obj in objToSpawn) {
			abilities.Add (obj.GetComponent<AbilityData> ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (testInput && Input.anyKey) {
			Debug.Log (Input.inputString);
		}
		if (Input.GetKey(KeyCode.KeypadPlus)) {
			currentYPosition += 1 * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.KeypadMinus)) {
			currentYPosition -= 1 * Time.deltaTime;
		}
		for (int a = 0; a < abilities.Count; a++) {
			AbilityData data = abilities [a];
			data.minusDeltaTime (Time.deltaTime);
			if (Input.anyKey) {
				if (data.activationKey1 != "") {
					if ((Input.GetKeyDown ((KeyCode)System.Enum.Parse (typeof(KeyCode), data.activationKey1)) && player1) ||
						(Input.GetKeyDown ((KeyCode)System.Enum.Parse (typeof(KeyCode), data.activationKey2)) && !player1)) {
						if (data.delay == 0) {
							if (data.currentRefreshTime <= 0) {
								data.activate (player.transform.position + new Vector3(data.initialOffset, 0, 0) +
								new Vector3 (Mathf.Abs (player.GetComponent<Rigidbody2D> ().velocity.x) * delay,
									currentYPosition, data.layer));
								data.refresh ();
							}
						} else {
							delay = data.delay;
						}
					}
				}
			}
		}
	}
}