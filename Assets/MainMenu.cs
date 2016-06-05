using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject tutorial;
	public GameObject cinema;

	public void showCinema() {
		cinema.SetActive (true);
	}

	// Use this for initialization
	void Start () {
		//		GameObject.Find ("SoundManager").GetComponent<SoundManager> ().playMenuTheme ();
		//GameObject.Find ("SoundManager").GetComponent<SoundManager> ().playCombatTheme ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadGame()
    {
        Application.LoadLevel(1);
    }

	public void showHideTutorial() {

		tutorial.SetActive (!tutorial.activeSelf);

	}

	public void quit() {
		Application.Quit();
	}

	public void StartGame() {
		showCinema ();
	}

}
