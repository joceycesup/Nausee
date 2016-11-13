using UnityEngine;
using System.Collections;

public class FinalDoor : Door {
	public AudioSource openSource;
	public AudioSource exitMusic;
	public float audioFadeTime = 1.0f;
	private float audioFadeRemainingTime = 0.0f;

	void Start () {
		isFinalDoor = true;
		openSource = gameObject.GetComponent<AudioSource> ();
		exitMusic = gameObject.AddComponent<AudioSource> ();
		exitMusic.clip = Resources.Load<AudioClip> ("Sounds/Exit_Music");
		exitMusic.loop = false;
		exitMusic.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (audioFadeRemainingTime <= 0.0f && exitMusic.isPlaying) {
			audioFadeRemainingTime = audioFadeTime;
		}
		if (audioFadeRemainingTime > 0.0f) {
			if ((audioFadeRemainingTime -= Time.deltaTime) <= 0.0f) {/*
				AudioSource tmp = audio1;
				audio1 = audio2;
				audio2 = tmp;
				audio1.volume = 1.0f;
				audio2.volume = 0.0f;
				audio2.Stop ();//*/
			} else {
				openSource.volume = audioFadeRemainingTime / audioFadeTime;
				exitMusic.volume = 1.0f - audioFadeRemainingTime / audioFadeTime;
			}
		}
	}

	public bool Open (GameObject k) {
		bool res = base.Open (k);
		exitMusic.PlayDelayed (openSound.length - audioFadeTime);
		return res;
	}
}
