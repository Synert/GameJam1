using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : AbilityData {

	public float force = 0;
	public Vector2 direction = Vector2.zero;

	public override void activate(Vector3 pos)
	{
		Rigidbody2D[] all = GameObject.FindObjectsOfType<Rigidbody2D> ();
		foreach (Rigidbody2D rig in all) {
			rig.AddForce (direction * force);
		}
	}
}
