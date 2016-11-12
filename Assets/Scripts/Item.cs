using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	private Animator animator;

	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		Debug.Log (gameObject + " : " + animator);
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			if (animator != null) {
				animator.enabled = true;
				Destroy (gameObject.GetComponent<BoxCollider2D> ());
			} else {
				Destroy (gameObject);
			}
		}
	}
}
