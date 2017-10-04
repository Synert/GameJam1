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
	public bool player2Start = false;
	public Text Score;
	public Text ScoreP1;
	public Text ScoreP2;
	public float points = 0;

	void Awake() {
		GameObject.DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		grab ();
	}

	public void grab () {
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
		Image[] Images = GameObject.FindObjectsOfType<Image> ();
		Text[] texts = GameObject.FindObjectsOfType<Text> ();
		for (int a = 1; a < abilities.Count + 1; a++) {
			for (int b = 0; b < Images.Length; b++) {
				if (Images [b].transform.name == "Image" + a) {
					abilities [a - 1].objImage = Images [b];
					Images [b].sprite = abilities [a - 1].UISprite;
				}
				if (Images [b].transform.name == "Key" + a) {
					abilities [a - 1].keyImage = Images [b];
					Images [b].sprite = abilities [a - 1].keySprite;
				}
			}
			for (int b = 0; b < texts.Length; b++) {
				if (texts [b].transform.name == "Text" + a) {
					abilities [a - 1].textVar = texts [b];
				}
				if (texts [b].transform.name == "Score") {
					Score = texts [b];
				}
				if (texts [b].transform.name == "ScoreP1") {
					ScoreP1 = texts [b];
				}
				if (texts [b].transform.name == "ScoreP2") {
					ScoreP2 = texts [b];
				}
			}
		}

		if (ScoreP1 != null) {
			ScoreP1.text = "0";
		}
		if (ScoreP2 != null) {
			ScoreP2.text = "0";
		}

		if (player1End == true) {
			player2Start = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindObjectOfType<MultiplayerDetector> ()) {
			if (ScoreP1 != null && ScoreP2 != null) {
				ScoreP1.text = "Player 1: " + Mathf.FloorToInt (player1Score).ToString ();
				ScoreP2.text = "Player 2: " + Mathf.FloorToInt (player2Score).ToString ();
			}
			if (player1 && !player1End) {
				player1Score += Time.deltaTime;
			} else if (player2 && player2Start) {
				player2Score += Time.deltaTime;
			}
			if (Score != null) {
				Score.text = "Points: " + points.ToString () + "\n" + "Delay: " + delay.ToString ();
			}
			for (int a = 0; a < abilities.Count; a++) {
				AbilityData data = abilities [a];
				data.minusDeltaTime (Time.deltaTime);
				if (Input.anyKey) {
					if (data.activationKey != "") {
						if (Input.GetKeyDown ((KeyCode)System.Enum.Parse (typeof(KeyCode), data.activationKey))) {
							if (data.delay == 0) {
								if (data.currentRefreshTime <= 0) {
									if (data.pointCost <= points) {
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
		} else {
			destroy ();
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
}