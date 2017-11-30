using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour {

	int damage;
	public float speed = 5;
	Transform playertr;
	int MaxDist = 3;

	// Use this for initialization
	void Start () {
		playertr = GameObject.FindWithTag ("Player").transform;
	}

	public void init(int amount, Vector2 dir)
	{
		damage = amount;
		GetComponent<Rigidbody2D> ().velocity = dir * speed;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Vector3.Distance (playertr.position, transform.position) > MaxDist) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) 
	{
		if (collider.gameObject.tag == "Enemy") {
			collider.gameObject.GetComponent<Health> ().takeDamage (damage);
			Destroy (gameObject);
		}

		if (collider.gameObject.tag == "Wall") {
			Destroy (gameObject);
		}
	}
}
