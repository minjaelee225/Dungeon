using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atkItem : Items {

	public AudioClip pickup;
	private PlayerStats stats;

	void Start () {
		stats = FindObjectOfType<PlayerStats> ();	
	}

	public override void improveStat()
	{
		stats.atk += 2;
		AudioSource.PlayClipAtPoint (pickup, transform.position);
	}

}
