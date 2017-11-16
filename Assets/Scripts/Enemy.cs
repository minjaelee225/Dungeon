using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 10f;
	public float attack1Range = 0.1f;
	public int attack1Damage = 1;
	public float timeBetweenAttacks;
	BoxCollider2D territory;
	GameObject player;
	bool playerInTerritory;



	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		territory = GetComponent<BoxCollider2D> ();
		playerInTerritory = false;
		Rest ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (playerInTerritory)
		{
			MoveToPlayer ();
		}

		else
		{
			Rest ();
		}
	}

	public void MoveToPlayer ()
	{
		//look at player
		//move towards player
		if (Vector3.Distance (transform.position, player.transform.position) > attack1Range) {
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);

		} else {
			player.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver); 
		}
	}

	public void Rest ()
	{

	}
	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("Hello I am here");
		if (other.gameObject == player)
		{
			playerInTerritory = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject == player) 
		{
			playerInTerritory = false;
		}
	}
}
