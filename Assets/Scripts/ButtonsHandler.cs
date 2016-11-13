using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonsHandler : MonoBehaviour {
	public GameObject background;
	public GameObject exitButton;
	public GameObject playButton;

	private Sprite eSprite;

	void OnGUI () {
		Sprite eSprite = exitButton.GetComponent<SpriteRenderer> ().sprite;
		Vector2 eSize = exitButton.GetComponent<BoxCollider2D>().bounds.extents * ((float)(Screen.width)) / ((float)(Camera.main.pixelWidth));
		Debug.Log (eSize);
		if (GUI.Button(new Rect(0, 0, eSize.x, eSize.y), eSprite.texture, GUIStyle.none))
			ExitButton ();
		/*
		Vector2 pSize = playButton.textureRect.size / 2.0f * (Screen.width / Camera.main.pixelWidth);
		if (GUI.Button (new Rect (eSize.x, 0, pSize.x, pSize.y), playButton.texture, GUIStyle.none))
			PlayButton ();//*/
	}

	void PlayButton () {
		Debug.Log("play");
	}

	void ExitButton () {
		Debug.Log("exit");
		Application.Quit ();
	}
}
