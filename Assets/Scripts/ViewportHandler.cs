using UnityEngine;
using System.Collections;

public class ViewportHandler : MonoBehaviour {
	private static GameObject _viewport;

	public static GameObject viewport {
		get {
			return _viewport;
		}
	}

	private GameObject m_tile;
	private bool m_moving = false;
	public float slideSpeed = 10;

	// audio variables
	private AudioSource audio1;
	private AudioSource audio2;
	public float audioFadeTime = 2.0f;
	private float audioFadeRemainingTime = 0.0f;

	// Use this for initialization
	void Start () {
		audio1 = gameObject.GetComponent<AudioSource> ();
		audio2 = (AudioSource)gameObject.AddComponent<AudioSource> ();
		audio2.loop = true;
		audio2.playOnAwake = false;

		gameObject.GetComponent<BoxCollider2D> ().size = new Vector2 (Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height, Camera.main.orthographicSize * 2.0f);
		_viewport = gameObject;

		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 16.0f / 9.0f;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = Camera.main;

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{  
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}

	void Update () {
		if (audioFadeRemainingTime > 0.0f) {
			if ((audioFadeRemainingTime -= Time.deltaTime) <= 0.0f) {
				AudioSource tmp = audio1;
				audio1 = audio2;
				audio2 = tmp;
				audio1.volume = 1.0f;
				audio2.volume = 0.0f;
				audio2.Stop ();
			} else {
				audio1.volume = audioFadeRemainingTime / audioFadeTime;
				audio2.volume = 1.0f - audioFadeRemainingTime / audioFadeTime;
			}
		}

		if (m_moving) {
			float distance = Vector3.Distance (gameObject.transform.position, m_tile.transform.position);
			if (distance > 0) {
				gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, m_tile.transform.position, Time.deltaTime *slideSpeed / distance);
			} else {
				m_moving = false;
			}
		}
	}

	public void MoveViewport (GameObject levelTile) {
		m_tile = levelTile;
		m_moving = true;
	}

	public void FadeToSound (int step, float fade) {
		if (step == 0) {
			Debug.Log ("Exit music");
			audio2.clip = Resources.Load<AudioClip> ("Sounds/Exit_Music");
		} else {
			Debug.Log ("Sounds/AMB" + step);
			audio2.clip = Resources.Load<AudioClip> ("Sounds/AMB" + step);
		}
		audio2.Play ();
		audioFadeTime = fade;
		audioFadeRemainingTime = audioFadeTime;
	}
}
