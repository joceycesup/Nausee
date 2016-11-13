using UnityEngine;
using System.Collections;

public class MenuLoader : MonoBehaviour {
	public GameObject canvas;

	void Start () {
		Instantiate (canvas);
	}
}
