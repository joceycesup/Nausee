using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public GameObject key;

	public bool Open (GameObject k) {
		if (key == k) {
			Destroy (gameObject.GetComponent<BoxCollider2D> ());
			return true;
		}
		return false;
	}
}
