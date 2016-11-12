using UnityEngine;
using System.Collections;

public class Statue : MonoBehaviour {
	public GameObject emitter;
	public float fadeTime = 2.0f;
	private float fadeRemainingTime = 0.0f;

	public void Seen () {
		fadeRemainingTime = fadeTime;
	}

	// Update is called once per frame
	void Update () {
		if (fadeRemainingTime > 0.0f) {
			if ((fadeRemainingTime -= Time.deltaTime) <= 0.0f) {
				gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
				emitter.GetComponent<StatueEmitter> ().StatueSeen ();
			} else {
				//Debug.Log (fadeRemainingTime / fadeTime);
				gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, fadeRemainingTime / fadeTime);
			}
		}
	}
}
