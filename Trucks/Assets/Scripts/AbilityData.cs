using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AbilityData : MonoBehaviour {

	public Sprite UISprite; 
	public Sprite keySprite; 
	public Image keyImage;
	public Image objImage;
	public Text textVar;
	public string activationKey;
	public float currentRefreshTime;
	public float refreshTime;
	public float pointGain;
	public float pointCost;
	public string description;
	public float initialOffset;
	public float delay;
	public float layer;
	public LayerMask layerMask;

	public Vector3 startPos1;
	public Vector3 hitPos1;

	public void minusDeltaTime(float time) {
		if (pointCost < pointGain) {
			textVar.text = "Gain: " + (pointGain - pointCost).ToString ();
		}
		else if (pointCost >= pointGain) {
			textVar.text = "Cost: " + (pointCost - pointGain).ToString ();
		}
		if (currentRefreshTime > 0) {
			currentRefreshTime -= time;
			float percentage = currentRefreshTime / refreshTime;
			if (keyImage != null) {
				keyImage.color = new Color (255, 255, 255, 1 - percentage);
			}
			if (objImage != null) {
				objImage.color = new Color (255, 255, 255, 1 - percentage);
			}
			if (textVar != null) {
				textVar.color = new Color (255, 255, 255, 1 - percentage);
			}
		} else {
			if (keyImage != null) {
				keyImage.color = new Color (255, 255, 255, 1);
			}
			if (objImage != null) {
				objImage.color = new Color (255, 255, 255, 1);
			}
			if (textVar != null) {
				textVar.color = new Color (255, 255, 255, 1);
			}
		}
	}

	void Update() {
		Debug.DrawLine (startPos1, hitPos1, Color.red);
	}

	public void refresh() {
		currentRefreshTime = refreshTime;
	}

	public void clearRefresh() {
		currentRefreshTime = 0;
	}

	public virtual void spawn(Vector3 pos) {
		GameObject.Instantiate (gameObject, pos + new Vector3(initialOffset, 0, 0), gameObject.transform.rotation);
	}

	public virtual void activate(Vector3 pos) {
		spawn (pos);
	}

}