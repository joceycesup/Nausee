using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	private Animator animator;

	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		//Debug.Log (gameObject + " : " + animator);
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			//Debug.Log ("item collision");
			Destroy (gameObject.GetComponent<BoxCollider2D> ());
			if (animator != null) {
				animator.enabled = true;
			} else {
				Destroy (gameObject, gameObject.GetComponent<AudioSource> ().clip.length);
			}
		}
		gameObject.GetComponent<AudioSource> ().Play ();
	}
}
