using UnityEngine;
using System.Collections;

public class MenuLoader : MonoBehaviour {
	public GameObject canvas;

	void Start () {
		UnityEngine.Cursor.visible = true;
		((GameObject)Instantiate (canvas)).GetComponent<Canvas> ().worldCamera = Camera.main;
	}
}
