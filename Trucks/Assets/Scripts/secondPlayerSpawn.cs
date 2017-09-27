using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondPlayerSpawn : MonoBehaviour {

	public List<abilityData> abilities = new List<abilityData>();
	public float delay = 0;
	public float currentYPosition = 0;
	GameObject player = null;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.KeypadPlus)) {
			currentYPosition += 1 * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.KeypadMinus)) {
			currentYPosition -= 1 * Time.deltaTime;
		}
		foreach (abilityData data in abilities) {
			data.minusDeltaTime (Time.deltaTime);
			if (data.activationKey != "") {
				if (Input.GetKeyDown (data.activationKey)) {
					if (data.delay != 0) {
						if (data.obj != null) {
							if (data.currentRefreshTime <= 0) {
								GameObject.Instantiate (data.obj, new Vector3 (player.transform.position.x + player.GetComponent<Rigidbody2D> ().velocity.x * delay, currentYPosition, data.layer), data.obj.transform.rotation);
							}
						}
					} else {
						delay = data.delay;
					}
				}
			}
		}
	}

}

[System.Serializable]
public struct abilityData {

	public Sprite UISprite; 
	public string activationKey;
	public float currentRefreshTime;
	public float refreshTime;
	public string description;
	public GameObject obj;
	public float delay;
	public float layer;

	public void minusDeltaTime(float time) {
		currentRefreshTime -= time;
	}

}