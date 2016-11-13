using UnityEngine;
using System.Collections;

public class Statue : MonoBehaviour {
	public GameObject emitter;
	public float fadeTime = 2.0f;
	private float fadeRemainingTime = 0.0f;

	public void Seen () {
		SetHalo (true);
		fadeRemainingTime = fadeTime;
	}

	public void SetHalo (bool b) {
		transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = b;
		transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = b?"HUD":"Items";
		gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = b?"HUD":"Items";
	}

	// Update is called once per frame
	void Update () {
		if (fadeRemainingTime > 0.0f) {
			if ((fadeRemainingTime -= Time.deltaTime) <= 0.0f) {
				gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
				transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
				emitter.GetComponent<StatueEmitter> ().StatueSeen ();
			} else {
				//Debug.Log (fadeRemainingTime / fadeTime);
				gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, fadeRemainingTime / fadeTime);
				transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color(1, 1, 1, fadeRemainingTime / fadeTime);
			}
		}
	}
}
