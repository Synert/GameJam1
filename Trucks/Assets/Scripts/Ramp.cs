using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : AbilityData {

	public override void spawn(Vector3 pos) {
		RaycastHit2D result = Physics2D.Raycast (pos, Vector2.down, 100, layerMask);
		if (result.collider != null) {
			if (!result.collider.GetComponent<AbilityData> ()) {
				create (pos, result.transform.position, result);
			}
		} else {
			result = Physics2D.Raycast (pos, Vector2.up, layerMask);
			if (result.collider != null) {
				if (!result.collider.GetComponent<AbilityData> ()) {
					create (pos, result.transform.position, result);
				}
			}
		}
	}

	void create(Vector3 pos, Vector3 hitPos, RaycastHit2D hit) {
		//debug
		hitPos1 = hitPos;
		startPos1 = pos;

		GameObject spawned = GameObject.Instantiate (gameObject, hit.transform.position, hit.collider.transform.rotation);
		//deal with scaling
		Vector3 scale = spawned.transform.localScale;
		scale.x = (hit.transform.localScale.x / 2) * 1;
		scale.y = (hit.transform.localScale.x / 2) * 1;
		spawned.transform.localScale = scale;

		//deal with positioning
		spawned.transform.Translate (hit.transform.up * hit.collider.bounds.size.y/3 * 2);
	}

}