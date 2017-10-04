using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour {

	public List<GameObject> objToSpawn = new List<GameObject>();
	List<AbilityData> abilities = new List<AbilityData>();
	public float delay = 0;
	public bool player1 = true;
	public bool player2 = false;
	public bool testInput = false;
	GameObject player = null;
	public float player1Score = 0;
	public float player2Score = 0;
	public bool player1End = false;
	public float points = 0;

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
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindObjectOfType<MultiplayerDetector>()) {
			if (player1 && !player1End) {
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
									if (data.pointCost < points) {
										data.activate (player.transform.position + new Vector3 (data.initialOffset, 0, 0) +
										new Vector3 (Mathf.Abs (player.GetComponent<Rigidbody2D> ().velocity.x) * delay,
											0, data.layer));
										data.refresh ();
										points += data.pointGain;
										points -= data.pointCost;
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
	}

									public void resetAbilities() {
		foreach (AbilityData ab in abilities) {
			ab.refresh ();
		}
	}

	public void destroy() {
		Destroy (this.gameObject);
	}

	void OnGUI() {
		if (GameObject.FindObjectOfType<MultiplayerDetector> ()) {
			foreach (AbilityData ab in abilities) {
				Color temp = GUI.color;
				GUI.color = new Color (255, 255, 255, 1);//EDIT THIS SO IT TAKES IN VALUES NEEDED
				GUI.DrawTexture (ab.image, ab.UISprite.texture);
				GUI.color = temp;
			}
		}
	}
}