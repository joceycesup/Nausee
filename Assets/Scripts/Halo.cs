using UnityEngine;
using System.Collections;

public class Halo : MonoBehaviour {

	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		SpriteRenderer[] srs = transform.GetComponentsInChildren<SpriteRenderer> ();
		for (int i = 0; i < srs.Length; ++i) {
			srs [i].enabled = true;
			srs [i].color = gameObject.GetComponent<SpriteRenderer> ().color;
		}
	}

	void OnApplicationFocus (bool focus) {
		//focus = false;
		gameObject.GetComponent<SpriteRenderer> ().enabled = focus;
		SpriteRenderer[] srs = transform.GetComponentsInChildren<SpriteRenderer> ();
		for (int i = 0; i < srs.Length; ++i) {
			srs [i].enabled = focus;
		}
	}

	void Update () {
	}

	public void SetSize (float size) {
		gameObject.GetComponent<SpriteRenderer> ().transform.localScale = new Vector3 (size, size);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Statue_1" || other.gameObject.tag == "Statue_2") {
			gameObject.transform.parent.gameObject.GetComponent<Character> ().SeesStatue (other.gameObject);
		}
	}
}
