﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkieTalkie : MonoBehaviour {
	private micInput micro;
	private float lastUpdateTime;
	private float currentTime;

	private List<float> loudnessValues;
	// utilise pour stocker les valeurs en cours de calibration du niveau moyen de la voix
	private float averageLoudness;
	// niveau moyen de la voix

	public float loudnessThreshold;
	// sensibilité du micro
	public float soundTimeBeforeDetect;
	//Vérifie qu'il y a un son toutes les x secondes;

	private AudioSource voiceOver;
	public float questionDelay = 2.0f;
	private int maxSkip = 2;
    
	// Use this for initialization
	void Start () {
		loudnessValues = new List<float> ();
		lastUpdateTime = Time.time;
		currentTime = Time.time;
		micro = gameObject.GetComponent<micInput> ();
		voiceOver = gameObject.GetComponent<AudioSource> ();
		voiceOver.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
	//	HandleVoiceOver ();
		if (currentTime > soundTimeBeforeDetect) {
			currentTime = 0;/*
			if (IsTalking ()) {
				gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.0f, 0.0f);
			} else {
				gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f);
			}//*/
			lastUpdateTime = Time.time;

			if (Input.GetButton ("CalibrateSound")) { // on commence la calibration du micro en appuyant sur ce bouton
				loudnessValues.Add (micro.volume);
			}
		} else {
			currentTime = Time.time - lastUpdateTime;
		}
		if (Input.GetButtonUp ("CalibrateSound")) { // on arrete la calibration du micro et on affecte la moyenne puis le seuil
			averageLoudness = 0.0f;
			foreach (float lv in loudnessValues) {
				averageLoudness += lv;
			}
			if (loudnessValues.Count > 0) { // evite d'avoir un NaN
				averageLoudness /= loudnessValues.Count;
			}
			loudnessValues.Clear ();

			// 2.0f -> valeur arbitraire, on met le seuil a la moitie de la moyenne du volume lors de la calibration
			// change cette valeur comme tu veux ou utilise une autre technique pour definir le seuil, c'est toi qui vois
			loudnessThreshold = averageLoudness / 2.0f;
			Debug.Log ("Threshold : " + loudnessThreshold);
		}
	}

	public bool IsTalking () {
		return micro.volume > loudnessThreshold || Input.GetButton ("Fire2");
	}

	private string IntToString (int v, int size) {
		string res = v.ToString ();
		int nb0 = size - res.Length;
		for (int i = 0; i < nb0; ++i) {
			res = "0" + res;
		}
		return res;
	}

	public void PlanQuestion (int question, int reminder) {
		voiceOver.clip = null;
		for (int i = 0; i < maxSkip && voiceOver.clip == null; ++i) {
			string path = "Sounds/VoiceOver/Q" + IntToString (question++, 2);
			if (reminder > 0) {
				path += "R" + reminder;
			}
			Debug.Log (path);
			voiceOver.clip = Resources.Load<AudioClip> (path);
		}
		voiceOver.PlayDelayed (questionDelay);
	}

	private void HandleVoiceOver () {
		if (!voiceOver.isPlaying && voiceOver.clip != null) {
			voiceOver.clip = null;
		}
	}
}
