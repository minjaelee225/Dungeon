using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public int maxHealth = 100;
	protected int health;
	// Use this for initialization
	void Start () {
		health = maxHealth;
	}

	// Update is called once per frame
	void Update () {

	}

	public virtual void takeDamage(int amount)
	{
		health -= amount;
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
}