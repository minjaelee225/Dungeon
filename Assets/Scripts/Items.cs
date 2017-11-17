using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D collider) 
	{
		if (collider.gameObject.tag == "Player") {
			improveStat ();
			Destroy (gameObject);
		}
	}

	public virtual void improveStat()
	{

	}
}
