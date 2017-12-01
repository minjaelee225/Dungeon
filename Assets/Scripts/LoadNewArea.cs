using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {
	
	levelManager lm;

	// Use this for initialization
	void Start () {
		lm = FindObjectOfType<levelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") 
		{
			lm.level += 1;
			if (lm.level % 5 == 0) {
				SceneManager.LoadScene (3);
			} else {
				SceneManager.LoadScene (2);
			}
		}
	}
}
