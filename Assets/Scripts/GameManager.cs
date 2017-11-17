using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int level;
	private static bool managerExists;

	// Use this for initialization
	void Start () 
	{
		level = 1;
		if (!managerExists) {
			managerExists = true;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


}
