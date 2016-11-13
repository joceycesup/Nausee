using UnityEngine;
using System.Collections;

public class FinalDoor : Door {
	public AudioSource exitMusic;

	void Start () {
		isFinalDoor = true;
		exitMusic = gameObject.AddComponent<AudioSource> ();
		exitMusic.clip = Resources.Load<AudioClip> ("Sounds/Exit_Music");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
