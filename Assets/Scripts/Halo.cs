using UnityEngine;
using System.Collections;

public class Halo : MonoBehaviour {

	void Start () {
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
