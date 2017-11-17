using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {
	
	GameManager gm;

	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") 
		{
			gm.level += 1;
			if (gm.level % 5 == 0) {
				SceneManager.LoadScene (2);
			} else {
				SceneManager.LoadScene (0);
			}
		}
	}
}
