using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistonPush : MonoBehaviour {

	public float timesPushForward = 0;
	public float timesPullBack = 0;
	public float distance = 0;
	public float currentDist = 0;
	public float delay = 0;
	public float currentDelay = 0;
	bool toggleAdd = false;

	// Update is called once per frame
	void Update () {
		if (currentDelay <= 0) {
            Debug.Log(GetComponent<Rigidbody2D>().velocity);
			if (currentDist >= 0 && !toggleAdd) {
				//transform.Translate (transform.forward * (Time.deltaTime * timesPushForward));
				//transform.position += (transform.up * Time.deltaTime * timesPushForward);
                GetComponent<Rigidbody2D>().velocity = transform.up * timesPushForward;
                currentDist -= Time.deltaTime * timesPushForward;
				if (currentDist <= 0)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    toggleAdd = !toggleAdd;
				}
			} else if (toggleAdd) {
                //transform.Translate (-transform.forward * (Time.deltaTime * timesPullBack));
                //transform.position -= (transform.up * Time.deltaTime * timesPullBack);
                GetComponent<Rigidbody2D>().velocity = -transform.up * timesPullBack;
                currentDist += Time.deltaTime * timesPullBack;

				if (currentDist >= distance) {
                   GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                   toggleAdd = !toggleAdd;
					currentDelay = delay;
				}
			}
		} else {
			currentDelay -= Time.deltaTime;
		}
	}
}
