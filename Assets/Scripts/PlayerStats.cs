using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public int atk;
	public int def;
	public int magic;
	private static bool managerExists;

	// Use this for initialization
	void Start () {
		atk = 1;
		def = 1;
		magic = 1;
		if (!managerExists) {
			managerExists = true;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
