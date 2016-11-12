using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public float maxHealth;
	public float stepDistance;
	public float haloMaxSize;

	private float health;
	private float stepsWalked = 0.0f;
	private Vector3 lastStepPosition;

	public float crysisRemainingTime = 0.0f;
	private float lastTalkTime;
	public float maxStopTalkTime = 0.2f;
	private bool wasTalking = true;
	private bool isTalking = true;
	private int crysis = 0;

	private bool doorCrysis = false;

	public float maxWellBeing;
	public float wellBeing;

	public float maxSpeed;
	public float minSpeed;

	public GameObject walkieTalkie;

	private GameObject key = null;
	private AudioSource audioSource;

	public float[] crysisTalkTimes;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		wellBeing = maxWellBeing;
		lastStepPosition = gameObject.transform.position;
		gameObject.GetComponentInChildren<Halo> ().SetSize (haloMaxSize);

		audioSource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		wasTalking = isTalking;
		isTalking = walkieTalkie.GetComponent<WalkieTalkie> ().IsTalking ();
		if (crysisRemainingTime > 0.0f) {
			if (isTalking) {
				float dTime = Time.deltaTime;
				if (!wasTalking) {
					Debug.Log ("started talking");
					if ((Time.time - lastTalkTime) < maxStopTalkTime) {
						dTime = Time.time - lastTalkTime;
					}
				}
				if ((crysisRemainingTime -= dTime) <= 0.0f) {
					StopCrysis ();
				}
				lastTalkTime = Time.time;
			} else if ((Time.time - lastTalkTime) > maxStopTalkTime) {
				Debug.Log ("stopped talking");
			}
		} else {
			float speed = ((wellBeing / maxWellBeing) * (maxSpeed - minSpeed)) + minSpeed;

			float dx = Input.GetAxis ("Horizontal");
			float dy = Input.GetAxis ("Vertical");
			Vector3 dv = Vector3.ClampMagnitude (new Vector3 (dx, dy, 0), 1.0f);
			dv *= Time.deltaTime * speed / stepDistance;
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
		GUI.Label (new Rect (10, 10, 200, 20), "Health     : " + (int)health);
		GUI.Label (new Rect (10, 30, 200, 20), "Steps      : " + (int)stepsWalked);
		GUI.Label (new Rect (10, 50, 200, 20), "Well Being : " + (int)wellBeing);
	}

	private void Death () {
		//Destroy (gameObject);
	}

	public void SeesStatue (GameObject statue) {
		Debug.Log (statue + " : take cover!!");
		if (statue.tag == "Statue_1") {
			LoseWellBeing (10.0f);
		} else {
			Crysis (true);
		}
	}

	public void TriggerEnter (Collider2D other) {
		switch (other.gameObject.tag) {
		case "Door":
			if (other.gameObject.GetComponent<Door>().Open (key)) {
				//Debug.Log ("Can get through door");
				Destroy (key);
				//Debug.Log (key);
			} else {
				if (!doorCrysis) {
					doorCrysis = true;
					Crysis (true);
				}
			}
			break;
		case "Key":
			PickUpKey (other.gameObject);
			break;//*
		case "LevelTile":
			ViewportHandler.viewport.GetComponent<ViewportHandler>().MoveViewport (other.gameObject);
			break;//*/
		case "Item_1":
			GainHealth (15.0f);
			Destroy (other.gameObject);
			break;//*/
		case "Item_2":
			GainHealth (25.0f);
			LoseWellBeing (6.0f);
			Destroy (other.gameObject);
			break;//*/
		default:
			Debug.Log (other);
			break;
		}
	}

	private void GainHealth (float points) {
		health += points;
		if (health > maxHealth)
			health = maxHealth;
	}

	private void LoseHealth (float points) {
		health -= points;
		if (health <= 0.0f)
			Death ();
	}

	private void LoseWellBeing (float points) {
		float tmpWB = wellBeing;
		wellBeing -= points;
		if (wellBeing < 0.0f)
			wellBeing = 0.0f;

		float f = maxWellBeing / 4.0f;
		if ((int)(wellBeing / f) != (int)(tmpWB / f)) {
			Crysis (false);
		}

		gameObject.GetComponentInChildren<Halo> ().SetSize (1.0f + (haloMaxSize - 1.0f) * wellBeing / maxWellBeing);
		//TODO trigger crysis
	}

	public void TriggerExit (Collider2D other) {
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

	private void Crysis (bool repeatLast) {
		LoseHealth (10.0f);
		crysisRemainingTime = crysisTalkTimes[crysis];
		Debug.Log ("Sounds/Crysis" + (crysis+1) + ".wav");
		audioSource.Stop ();
		audioSource.clip = Resources.Load<AudioClip> ("Sounds/Crysis" + (crysis+1));
		audioSource.Play ();

		if (!repeatLast) {
			crysis++;
		}
	}

	private void StopCrysis () {
		audioSource.Stop ();
		WalkieTalkie wt = walkieTalkie.GetComponent<WalkieTalkie> ();
		wt.PlanQuestion ();
	}
}
