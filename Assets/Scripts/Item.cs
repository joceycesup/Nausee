using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	private Animator animator;

	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		//Debug.Log (gameObject + " : " + animator);
	}

	public void Pickup () {
		Debug.Log (gameObject+" : picked up");
		Destroy (gameObject.GetComponent<BoxCollider2D> ());
		if (animator != null) {
			animator.enabled = true;
		}
		gameObject.GetComponent<AudioSource> ().Play ();
	}
}
