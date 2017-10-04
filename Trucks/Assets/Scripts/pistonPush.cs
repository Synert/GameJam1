using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistonPush : MonoBehaviour {

	[SerializeField]
	private float timesPushForward = 0;
	[SerializeField]
	private float timesPullBack = 0;
	[SerializeField]
	private float distance = 0;
	[SerializeField]
	private float currentDist = 0;
	[SerializeField]
	private float delay = 0;
	[SerializeField]
	private float currentDelay = 0;
	private Vector3 max;
	private Vector3 min;
	private bool toggleAdd = false;

	void Start() {
		if (currentDist == 0) {
			max = transform.position;
			min = transform.position - (Vector3.up * distance);
		} else if (currentDist == distance) {
			min = transform.position;
			max = transform.position + (Vector3.up * distance);
		} else {
			max = transform.position + (Vector3.up * (distance - currentDist));
			min = transform.position - (Vector3.up * (currentDist));
		}
	}

	// Update is called once per frame
	void Update () {
		if (currentDelay <= 0) {
			if (currentDist >= 0 && !toggleAdd) {
                GetComponent<Rigidbody2D>().velocity = transform.up * timesPushForward;
                currentDist -= Time.deltaTime * timesPushForward;
				if (currentDist < 0)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    toggleAdd = !toggleAdd;
					currentDist = 0;
					transform.position = max;
				}
			} else if (toggleAdd) {
                GetComponent<Rigidbody2D>().velocity = -transform.up * timesPullBack;
                currentDist += Time.deltaTime * timesPullBack;

				if (currentDist > distance)
				{
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    toggleAdd = !toggleAdd;
					currentDelay = delay;
					transform.position = min;
				}
			}
		} else {
			currentDelay -= Time.deltaTime;
		}
	}
}
