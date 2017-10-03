using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerManager : MonoBehaviour {

	public List<GameObject> objToSpawn = new List<GameObject>();
	List<AbilityData> abilities = new List<AbilityData>();
	public float delay = 0;
	public bool player1 = false;
	public bool player2 = false;
	public bool testInput = false;
	GameObject player = null;
	public float player1Score = 0;
	public float player2Score = 0;

	void Awake() {
		GameObject.DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		MultiplayerManager[] temp = GameObject.FindObjectsOfType<MultiplayerManager> ();
		if (temp.Length != 1 && player1Score == 0) {
			destroy ();
		}
		player = GameObject.FindGameObjectWithTag ("Player");
		foreach (GameObject obj in objToSpawn) {
			if (obj != null) {
				abilities.Add (obj.GetComponent<AbilityData> ());
			}
		}
		if (player1Score != 0) {
			player2 = true;
			player1 = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (testInput && Input.anyKey) {
			Debug.Log (Input.inputString);
		}
		if (player1) {
			player1Score += Time.deltaTime;
		} else if (player2) {
			player2Score += Time.deltaTime;
		}
		for (int a = 0; a < abilities.Count; a++) {
			AbilityData data = abilities [a];
			data.minusDeltaTime (Time.deltaTime);
			if (Input.anyKey) {
				if (data.activationKey != "") {
					if (Input.GetKeyDown ((KeyCode)System.Enum.Parse (typeof(KeyCode), data.activationKey))) {
						if (data.delay == 0) {
							if (data.currentRefreshTime <= 0) {
								data.activate (player.transform.position +
								new Vector3 (Mathf.Abs (player.GetComponent<Rigidbody2D> ().velocity.x) * delay,
									0, data.layer));
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

	public void destroy() {
		Destroy (this.gameObject);
	}
}