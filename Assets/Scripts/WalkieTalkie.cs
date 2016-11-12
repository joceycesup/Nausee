using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkieTalkie : MonoBehaviour {
	private AudioSource microphone;
	private int sampleDataLength = 2048;
	private float updateStep = 0.1f; // delai de mise a jour des valeurs
	private float currentUpdateTime = 0.0f;
	private float clipLoudness; // volume moyen enregistre par le micro pendant un laps de temps donne (updateStep)
	private float loudnessThreshold; // seuil au-dela duquel on considere que le joueur parle
	private float[] clipSampleData; // tampon pour recuperer les donnees sonores du micro

	private float maxLoudness = 0;

	private List<float> loudnessValues; // utilise pour stocker les valeurs en cours de calibration du niveau moyen de la voix
	private float averageLoudness; // niveau moyen de la voix

	// Use this for initialization
	void Start () {
		loudnessValues = new List<float> ();

		//voiceOver = gameObject.GetComponent<AudioSource> (); // ne fais pas gaffe a ca, sera utilise plus tard
		microphone = gameObject.AddComponent<AudioSource> (); // le micro doit etre associe a une audiosource pour pouvoir enregistrer
		clipSampleData = new float[sampleDataLength];
		StartMicrophone ();
	}

	void OnApplicationFocus (bool hasFocus) { // pour éviter d'avoir un délai sur le micro en arretant l'execution ou en perdant le focus sur unity
		if (microphone != null) {
			if (hasFocus) {
				StartMicrophone ();
			} else {
				microphone.Pause ();
			}
		}
	}

	void StartMicrophone () {
		microphone.clip = Microphone.Start (null, true, 10, 44100);
		microphone.loop = true;
		//microphone.mute = true;
		microphone.volume = 0;
		while (!(Microphone.GetPosition (null) > 0)) {}
		microphone.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep) { // tous les updateStep, on actualise le volume moyen
			currentUpdateTime = 0.0f;
			microphone.clip.GetData (clipSampleData, microphone.timeSamples);
			clipLoudness = 0.0f;
			foreach (var sample in clipSampleData) {
				clipLoudness += Mathf.Abs (sample);
			}
			clipLoudness /= sampleDataLength;

			// on affiche le clipLoudness courant en appuyant sur cette touche, evite d'etre envahi par les Debug
			if (Input.GetButton("Fire2"))
				Debug.Log ("Loudness : " + clipLoudness);
			if (clipLoudness > maxLoudness) {
				maxLoudness = clipLoudness;
			}

			//gameObject.transform.Translate (new Vector3 (0, Time.deltaTime * clipLoudness * 5.0f, 0));
			Color c = new Color(1, IsTalking()?0:1, IsTalking()?0:1);
			gameObject.GetComponent<SpriteRenderer> ().color = c;

			if (Input.GetButton ("Fire1")) { // on commence la calibration du micro en appuyant sur ce bouton
				loudnessValues.Add (clipLoudness);
			}
		}
		if (Input.GetButtonUp ("Fire1")) { // on arrete la calibration du micro et on affecte la moyenne puis le seuil
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
			Debug.Log ("Average : " + averageLoudness);
		}
	}

	public bool IsTalking () {
		return clipLoudness > loudnessThreshold;
	}
}
