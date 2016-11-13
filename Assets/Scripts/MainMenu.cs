using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {


	public void Startgame () {
		SceneManager.LoadScene("NauseeLvl");
	}

	public void Quitgame () {
		Application.Quit();

	}
}
