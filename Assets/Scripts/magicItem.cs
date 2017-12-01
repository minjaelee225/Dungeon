using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicItem : Items {

	public AudioClip pickup;
	private PlayerStats stats;

	void Start () {
		stats = FindObjectOfType<PlayerStats> ();	
	}

	public override void improveStat()
	{
		stats.magic += 2;
		AudioSource.PlayClipAtPoint (pickup, transform.position);
	}
}
