using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

	public override void takeDamage(int amount)
	{
		base.takeDamage (amount);
		//textObject.GetComponent<Text> ().text = "Player Health: " + Health;
	}
}
