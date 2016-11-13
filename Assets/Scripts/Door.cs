using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public GameObject key;
	public Sprite openSprite;
	public AudioClip openSound;
	public bool isFinalDoor;

	public bool Open (GameObject k) {
		if (key == k) {
			Destroy (gameObject.GetComponent<BoxCollider2D> ());
			gameObject.GetComponent<SpriteRenderer> ().sprite = openSprite;
			gameObject.GetComponent<AudioSource> ().clip = openSound;
		}
		gameObject.GetComponent<AudioSource> ().Play ();
		return (key == k);
	}
}
