using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health {
	public GameObject textObject;
	public override void takeDamage(int amount)
	{
		base.takeDamage(amount);
		textObject.GetComponent<Text>().text = "Player Health: " + health;
	}
}