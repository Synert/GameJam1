using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityData : MonoBehaviour {

	public Sprite UISprite; 
	public string activationKey1;
	public string activationKey2;
	public float currentRefreshTime;
	public float refreshTime;
	public string description;
	public float initialOffset;
	public float delay;
	public float layer;
	public LayerMask layerMask;

	public Vector3 startPos1;
	public Vector3 hitPos1;

	public void minusDeltaTime(float time) {
		if (currentRefreshTime > 0) {
			currentRefreshTime -= time;
		}
	}

	void Update() {
		Debug.DrawLine (startPos1, hitPos1, Color.red);
	}

	public void refresh() {
		currentRefreshTime = refreshTime;
	}

	public virtual void spawn(Vector3 pos) {
		GameObject.Instantiate (gameObject, pos + new Vector3(initialOffset, 0, 0), gameObject.transform.rotation);
	}

	public virtual void activate(Vector3 pos) {
		spawn (pos);
	}

}