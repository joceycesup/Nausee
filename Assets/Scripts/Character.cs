using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public float maxHealth;
	private float health;

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
	}
	
	// Update is called once per frame
	void Update () {
		float speed = ((wellBeing / maxWellBeing) * (maxSpeed - minSpeed)) + minSpeed;

		float dx = Input.GetAxis ("Horizontal");
		float dy = Input.GetAxis ("Vertical");
		Vector3 dv = Vector3.ClampMagnitude (new Vector3 (dx, dy, 0), 1.0f);
		dv *= Time.deltaTime * speed;
		if ((health -= dv.magnitude) <= 0) {// distance = dv.magnitude;
			Death ();
		} else {
			gameObject.transform.Translate (dv);
		}
	}

	private void Death () {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D other) {
		switch (other.gameObject.tag) {
		case "Door":
			if (other.gameObject.GetComponent<Door>().RequiredKey() == key) {
				Debug.Log ("Can get through door");
				DestroyObject (key);
				Debug.Log (key);
			} else {
				Debug.Log ("OMG I'm having a crysis!");
			}
			break;
		case "Key":
			PickUpKey (other.gameObject);
			break;
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
}
