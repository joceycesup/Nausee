using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public GameObject key;
	public Sprite openSprite;

	public bool Open (GameObject k) {
		if (key == k) {
			Destroy (gameObject.GetComponent<BoxCollider2D> ());
			gameObject.GetComponent<SpriteRenderer> ().sprite = openSprite;
			return true;
		}
		return false;
	}
}
