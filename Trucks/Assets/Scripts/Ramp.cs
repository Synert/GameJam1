using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : AbilityData {

	public override void spawn(Vector3 pos) {
		RaycastHit2D result = Physics2D.Raycast (pos, Vector2.down, 100, layerMask);
		if (result.collider != null) {
			create (pos, result.transform.position, result);
		} else {
			result = Physics2D.Raycast (pos, Vector2.up, layerMask);
			if (result.collider != null) {
				create (pos, result.transform.position, result);
			}
		}
	}

	void create(Vector3 pos, Vector3 hitPos, RaycastHit2D hit) {
		Vector3 spawnPos = new Vector3 (pos.x + initialOffset, hit.point.y, pos.z);
		GameObject spawned = GameObject.Instantiate (gameObject, spawnPos, gameObject.transform.rotation);
		spawnPos.y += spawned.GetComponent<Collider2D> ().bounds.size.y/2;
		spawned.transform.position = spawnPos;
	}

}