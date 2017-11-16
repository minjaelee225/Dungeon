using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {
	PlayerController player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") 
		{
			player.level += 1;
			if (player.level % 5 == 0) {
				SceneManager.LoadScene (2);
			} else {
				SceneManager.LoadScene (1);
			}
		}
	}
}
