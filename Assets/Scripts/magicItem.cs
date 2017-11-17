using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicItem : Items {

	private PlayerStats stats;

	void Start () {
		stats = FindObjectOfType<PlayerStats> ();	
	}

	public override void improveStat()
	{
		stats.magic += 2;
	}
}
