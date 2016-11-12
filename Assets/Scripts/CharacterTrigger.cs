using UnityEngine;
using System.Collections;

public class CharacterTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		gameObject.transform.parent.gameObject.GetComponent<Character> ().TriggerEnter (other);
	}

	void OnTriggerExit2D (Collider2D other) {
		gameObject.transform.parent.gameObject.GetComponent<Character> ().TriggerExit (other);
	}
}
