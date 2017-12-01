using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public Texture backgroundTexture;


	void OnGUI(){

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

		GUI.Button (new Rect (Screen.width * .25f, Screen.height * .25f, Screen.width * .5f, Screen.height * .1f), "Mystery Dungeon, But Better");

		if (GUI.Button (new Rect (Screen.width * .25f, Screen.height * .5f, Screen.width * .5f, Screen.height * .1f), "Play Game")) {
			SceneManager.LoadScene (3);
		}
		if (GUI.Button (new Rect (Screen.width * .25f, Screen.height * .65f, Screen.width * .5f, Screen.height * .1f), "Options")) {
			print ("Clicked Options");
		}
		if (GUI.Button (new Rect (Screen.width * .25f, Screen.height * .8f, Screen.width * .5f, Screen.height * .1f), "Exit")) {
			SceneManager.LoadScene (0);
		}
	}
}
