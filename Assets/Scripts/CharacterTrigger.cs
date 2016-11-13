using UnityEngine;
using System.Collections;

public class CharacterTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<BoxCollider2D>().size = gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.size*1.01f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		//Debug.Log ("char trigger : " + other.tag);
		gameObject.transform.parent.gameObject.GetComponent<Character> ().TriggerEnter (other);
	}

	void OnTriggerExit2D (Collider2D other) {
		gameObject.transform.parent.gameObject.GetComponent<Character> ().TriggerExit (other);
	}
}
