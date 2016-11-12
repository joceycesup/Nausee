using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public float maxHealth;
	public float stepDistance;
	private float health;
	private float stepsWalked = 0.0f;
	private Vector3 lastStepPosition;

	private float crysisRemainingTime = 0.0f;

	public float maxWellBeing;
	public float wellBeing;

	public float maxSpeed;
	public float minSpeed;

	public GameObject key = null;

	public float[] crysisTalkTimes;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		wellBeing = maxWellBeing;
		lastStepPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (crysisRemainingTime > 0.0f) {
		} else {
			float speed = ((wellBeing / maxWellBeing) * (maxSpeed - minSpeed)) + minSpeed;

			float dx = Input.GetAxis ("Horizontal");
			float dy = Input.GetAxis ("Vertical");
			Vector3 dv = Vector3.ClampMagnitude (new Vector3 (dx, dy, 0), 1.0f);
			dv *= Time.deltaTime * speed / stepDistance;
			float steps = dv.magnitude;
			gameObject.transform.Translate (dv);
			if (Vector3.Distance (lastStepPosition, gameObject.transform.position) >= stepDistance) {
				lastStepPosition = gameObject.transform.position;
				stepsWalked++;
				if ((health--) <= 0) {
					Death ();
				}
			}
		}
	}

	void OnGUI () {
		GUI.Label (new Rect (10, 10, 100, 20), "Health : " + (int)health);
		GUI.Label (new Rect (10, 30, 100, 20), "Steps  : " + (int)stepsWalked);
	}

	private void Death () {
		//Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D other) {
		switch (other.gameObject.tag) {
		case "Door":
			if (other.gameObject.GetComponent<Door>().Open (key)) {
				//Debug.Log ("Can get through door");
				DestroyObject (key);
				//Debug.Log (key);
			} else {
				//Debug.Log ("OMG I'm having a crysis!");
				Crysis (0.0f);
			}
			break;
		case "Key":
			PickUpKey (other.gameObject);
			break;//*
		case "LevelTile":
			ViewportHandler.viewport.GetComponent<ViewportHandler>().MoveViewport (other.gameObject);
			break;//*/
		default:
			Debug.Log (other);
			break;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "LevelTile") {
//			ViewportHandler.viewport.GetComponent<ViewportHandler>().MoveViewport (gameObject.GetComponent<BoxCollider2D>().);
			Collider2D[] colls = Physics2D.OverlapPointAll(new Vector2(transform.position.x, transform.position.y));
			for (int i = 0; i < colls.Length; ++i) {
				if (colls [i].gameObject.tag == "LevelTile") {
					ViewportHandler.viewport.GetComponent<ViewportHandler>().MoveViewport (colls [i].gameObject);
					i = colls.Length;
				}
			}
		}
	}

	private void PickUpKey (GameObject keyObject) {
		key = keyObject;
		Vector3 keyExtents = key.GetComponent<SpriteRenderer> ().sprite.bounds.extents;
		key.transform.position = ViewportHandler.viewport.transform.position + new Vector3 (keyExtents.x - Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize - keyExtents.y, 0);
		key.GetComponent<SpriteRenderer> ().sortingLayerName = "HUD";
		key.transform.parent = ViewportHandler.viewport.transform;
		Destroy (key.GetComponent<BoxCollider2D> ());
	}

	private void Crysis (float value) {
		crysisRemainingTime += value;
	}
}
