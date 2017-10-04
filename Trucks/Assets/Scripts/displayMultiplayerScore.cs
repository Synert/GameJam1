using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayMultiplayerScore : MonoBehaviour {

	MultiplayerManager multi = null;
	Text mainText = null;
	string mainTextString = "";

	// Use this for initialization
	void Start () {
		mainText = GameObject.FindGameObjectWithTag ("MultiplayerMainText").GetComponent<Text>();
		multi = GameObject.FindObjectOfType<MultiplayerManager> ();
		if (multi != null) {
			multi.points = 0;
			multi.resetAbilities ();

			mainTextString += "Score:\n";
			mainTextString += "Player 1: ";
			mainTextString += Mathf.FloorToInt(multi.player1Score).ToString ();
			mainTextString += "\n Player 2:";
			mainTextString += Mathf.FloorToInt(multi.player2Score).ToString ();

			if (multi.player1 && multi.player2) {
				Destroy(GameObject.FindGameObjectWithTag ("PlayerReadyButton"));
				mainTextString += "\n";
				int player1 = Mathf.FloorToInt (multi.player1Score);
				int player2 = Mathf.FloorToInt (multi.player2Score);
				if (multi.player1Score < multi.player2Score) {
					mainTextString += "Player one trailed behind by " + (player2 - player1).ToString ();
				} else if (multi.player1Score > multi.player2Score) {
					mainTextString += "Player two trailed behind by " + (player1 - player2).ToString ();
				} else {
					mainTextString += "It's a tie, I guess both of you can pull your weight";
				}
			}
			if (multi.player1 && !multi.player2) {
				multi.player2 = true;
				multi.player1End = true;
			}
		}
		mainText.text = mainTextString;
	}
}
