using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMoving : MonoBehaviour {

	public GameObject obj = null;
	public float speed = 1;
	public float points = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D> ().AddForce (Vector2.right * speed);
		points += GetComponent<Rigidbody2D> ().velocity.x;
		if (Input.GetKeyDown (KeyCode.E)) {
			if (obj != null) {
				if (points >= obj.GetComponent<Data> ().objData.cost) {
					points -= obj.GetComponent<Data> ().objData.cost;
					GameObject temp = GameObject.Instantiate (obj, transform.position, obj.transform.rotation);
					temp.transform.position = temp.transform.position - new Vector3 (0, 0, 2);
					temp.transform.position += new Vector3 (GetComponent<Rigidbody2D> ().velocity.x, 0, 0);
				}
			}
		}
	}
}
