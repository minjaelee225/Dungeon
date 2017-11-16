using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {
	int damage;

	// Use this for initialization
	void Start () {
			
	}

	public void init(int amount)
	{
		damage = amount;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider) 
	{
		if (collider.gameObject.tag == "Enemy") {
			collider.gameObject.GetComponent<Health> ().takeDamage (damage);
			Destroy (gameObject);
		}
	}
}
