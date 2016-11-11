using UnityEngine;
using System.Collections;

public class WalkieTalkie : MonoBehaviour {
	private AudioSource microphone;
	private int sampleDataLength = 1024;
	private float updateStep = 0.1f;
	private float currentUpdateTime = 0.0f;
	private float clipLoudness;
	private float loudnessThreshold;
	private float[] clipSampleData;

	private float maxLoudness = 0;

	// Use this for initialization
	void Start () {
		microphone = gameObject.GetComponent<AudioSource> ();
		clipSampleData = new float[sampleDataLength];
		//Debug.Log (Microphone.devices [0]);
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
		if (currentUpdateTime >= updateStep) {
			currentUpdateTime = 0.0f;
			microphone.clip.GetData (clipSampleData, microphone.timeSamples);
			clipLoudness = 0.0f;
			foreach (var sample in clipSampleData) {
				clipLoudness += Mathf.Abs (sample);
			}
			clipLoudness /= sampleDataLength;
			//Debug.Log (clipLoudness);
			if (clipLoudness > maxLoudness) {
				maxLoudness = clipLoudness;
			}

			//gameObject.transform.Translate (new Vector3 (0, Time.deltaTime * clipLoudness * 5.0f, 0));
			Color c = new Color(1, maxLoudness-clipLoudness, maxLoudness-clipLoudness);
			gameObject.GetComponent<SpriteRenderer> ().color = c;
		}
	}

	public bool IsTalking () {
		return clipLoudness > loudnessThreshold;
	}
}
