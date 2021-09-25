using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public void Update() {
		if (Input.GetKeyDown(KeyCode.W) && !Physics2D.CircleCast((Vector2)transform.position + Vector2.up, 0.25f, Vector2.up, 0.25f)) {
			transform.Translate(Vector3.up);
		}
		if (Input.GetKeyDown(KeyCode.A) && !Physics2D.CircleCast((Vector2)transform.position + Vector2.left, 0.25f, Vector2.left, 0.25f)) {
			transform.Translate(Vector3.left);
		}
		if (Input.GetKeyDown(KeyCode.S) && !Physics2D.CircleCast((Vector2)transform.position + Vector2.down, 0.25f, Vector2.down, 0.25f)) {
			transform.Translate(Vector3.down);
		}
		if (Input.GetKeyDown(KeyCode.D) && !Physics2D.CircleCast((Vector2)transform.position + Vector2.right, 0.25f, Vector2.right, 0.25f)) {
			transform.Translate(Vector3.right);
		}
	}
}
