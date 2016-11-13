using UnityEngine;
using System.Collections;

public class PlayerHalo : MonoBehaviour {
	public float fadeTime = 2.0f;
	private float fadeRemainingTime = 0.0f;

	private Vector3 shrinkInitialSize;
	private float minSize = 0.01f;

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
		if (fadeRemainingTime > 0.0f) {
			if ((fadeRemainingTime -= Time.deltaTime) <= 0.0f) {
				//gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
				//emitter.GetComponent<StatueEmitter> ().StatueSeen ();
			} else {
				//Debug.Log (fadeRemainingTime / fadeTime);
				gameObject.GetComponent<SpriteRenderer> ().transform.localScale = shrinkInitialSize*(minSize+(fadeRemainingTime / fadeTime)*(1.0f-minSize));
			}
		}
	}

	public void SetSize (float size) {
		gameObject.GetComponent<SpriteRenderer> ().transform.localScale = new Vector3 (size, size);
	}

	public void Shrink (float ft) {
		fadeTime = ft;
		shrinkInitialSize = gameObject.GetComponent<SpriteRenderer> ().transform.localScale;
		fadeRemainingTime = fadeTime;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Statue_1" || other.gameObject.tag == "Statue_2") {
			gameObject.transform.parent.gameObject.GetComponent<Character> ().SeesStatue (other.gameObject);
		}
	}
}/*/
using UnityEngine;
using System.Collections;

public class Halo : MonoBehaviour {
	public GameObject side;

	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		Bounds b = gameObject.GetComponent<SpriteRenderer> ().bounds;
		for (int i = 0; i < 4; ++i) {
			GameObject child = (GameObject)Instantiate (side);
			child.transform.parent = gameObject.transform;
			child.transform.position = new Vector3 (
				b.center.x + b.extents.x * ((i==1||i==2)?1.0f:-1.0f),
				b.center.y + b.extents.y * ((i<2)?1.0f:-1.0f), 0.0f);


			SpriteRenderer sr = child.GetComponent<SpriteRenderer> ();
			sr.sprite = Sprite.Create (child.GetComponent<SpriteRenderer>().sprite.texture, new Rect (0, 0, 1, 1), new Vector2 ((i<2)?0.0f:1.0f, ((i%2)==0)?0.0f:1.0f), 0.1f);
			sr.enabled = true;
			sr.color = gameObject.GetComponent<SpriteRenderer> ().color;
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
}//*/