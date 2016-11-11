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

	// Use this for initialization
	void Start () {
		microphone = gameObject.GetComponent<AudioSource> ();
		//Debug.Log (Microphone.devices [0]);
		microphone.clip = Microphone.Start (Microphone.devices[0], true, 10, 44100);
		while (!(Microphone.GetPosition (Microphone.devices [0]) > 0)) {
		}
		microphone.Play ();

		clipSampleData = new float[sampleDataLength];
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

			//gameObject.transform.Translate (new Vector3 (0, Time.deltaTime * clipLoudness, 0));
		}
	}

	public bool IsTalking () {
		return clipLoudness > loudnessThreshold;
	}
}
