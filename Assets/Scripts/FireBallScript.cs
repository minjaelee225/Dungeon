﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour {

	int damage;
	public float speed = 8;

	// Use this for initialization
	void Start () {

	}

	public void init(int amount, Vector2 dir)
	{
		damage = amount;
		GetComponent<Rigidbody2D> ().velocity = dir * speed;
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
