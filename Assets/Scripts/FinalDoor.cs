using UnityEngine;
using System.Collections;

public class FinalDoor : Door {
	public AudioSource openSource;
	public AudioSource exitMusic;
	private float exitMusicStartTime;
	public float audioFadeTime = 1.0f;
	private float audioFadeRemainingTime = 0.0f;

	private float fadeOutTime;
	private float fadeOutStartTime;

	void Start () {
		exitMusicStartTime = float.MaxValue;
		isFinalDoor = true;
		openSource = gameObject.GetComponent<AudioSource> ();
		exitMusic = gameObject.AddComponent<AudioSource> ();
		//exitMusic.Play ();
		exitMusic.clip = Resources.Load<AudioClip> ("Sounds/Exit_Music");
		exitMusic.volume = 0.0f;
		exitMusic.loop = false;
		exitMusic.playOnAwake = false;
		exitMusic.Stop ();
	}

	void Update () {
		if (!exitMusic.isPlaying) {
			if (Time.time >= exitMusicStartTime) {
				exitMusic.Play ();
			}
		}
		if (exitMusic.isPlaying) {
			float volume = 1.0f - Mathf.Max(1.0f, Time.time-fadeOutStartTime)/(fadeOutTime-fadeOutStartTime);
			if ((audioFadeRemainingTime -= Time.deltaTime) <= 0.0f) {//*
				exitMusic.volume = volume;
				openSource.volume = 0.0f;
				openSource.Stop ();//*/
			} else {
				openSource.volume = audioFadeRemainingTime / audioFadeTime;
				exitMusic.volume = volume - audioFadeRemainingTime / audioFadeTime;
			}
		}
	}

	public override bool Open (GameObject k) {
		bool res = base.Open (k);
		if (res) {
			exitMusicStartTime = Time.time + openSound.length - audioFadeTime;
			audioFadeRemainingTime = audioFadeTime;
			fadeOutStartTime = Time.time + exitMusic.clip.length;
			fadeOutTime = Time.time + exitMusic.clip.length * 2.0f;
		}
		return res;
	}
}
