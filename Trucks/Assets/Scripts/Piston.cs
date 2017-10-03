using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : AbilityData {

	public override void spawn(Vector3 pos) {
		RaycastHit2D result = Physics2D.Raycast (pos, Vector2.down, 100, layerMask);
		if (result.collider != null) {
			if (!result.collider.GetComponent<AbilityData> ()) {
				create (pos, result.transform.position, result);
			}
		} else {
			result = Physics2D.Raycast (pos, Vector2.up, 100, layerMask);
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

		//deal with positioning
		//spawned.transform.Translate ((spawned.transform.up * spawned.transform.localScale.y) * GetComponent<SpriteRenderer>().sprite.bounds.size.y/2);
	}
}