using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void Startgame () {
		SceneManager.LoadScene(1);
	}

	public void Quitgame () {
		Application.Quit();
	}
}
