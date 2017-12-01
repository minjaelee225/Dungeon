using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour {

	public int level;
	private static bool managerExists;
	public AudioClip bgm;

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
		AudioSource.PlayClipAtPoint(bgm, transform.position,0.2f);
	}

	// Update is called once per frame
	void Update () 
	{

	}


}