using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 2.0f;
	public float attack1Range = 0.1f;
	public int attack1Damage = 1;
	public float timeBetweenAttacks;
	BoxCollider2D territory;
	GameObject player;
	bool playerInTerritory;
	Vector3[] movement;

	Vector3 pos;
	Vector3 old;
	Vector3 oold;
	Transform tr;

	// Use this for initialization
	void Start ()
	{
		pos = transform.position;
		tr = transform;

		player = GameObject.FindGameObjectWithTag ("Player");
		territory = GetComponent<BoxCollider2D> ();
		playerInTerritory = false;
		Rest ();

		movement = new Vector3[4];
	}

	// Update is called once per frame
	/*void Update ()
	{
		if (playerInTerritory) {
			MoveToPlayer ();
		} else {
			Rest ();
		}
	}*/

	void FixedUpdate()
	{
		oold = old;
		old = pos;
		if (Input.GetKey(KeyCode.UpArrow) && tr.position == pos)
		{
			Move();
		}
		else if (Input.GetKey(KeyCode.RightArrow) && tr.position == pos)
		{
			Move();
		}
		else if (Input.GetKey(KeyCode.DownArrow) && tr.position == pos)
		{
			Move();
		}
		else if (Input.GetKey(KeyCode.LeftArrow) && tr.position == pos)
		{
			Move();
		}
		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
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

	public void Move ()
	{
		Vector3 down = Vector3.down/1;
		Vector3 up = Vector3.up/1;
		Vector3 left = Vector3.left/1;
		Vector3 right = Vector3.right/1;
		movement [0] = down;
		movement [1] = up;
		movement [2] = left;
		movement [3] = right;
		int i = Random.Range (0, movement.Length);
		pos += movement [i];

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

	void OnCollisionEnter2D(Collision2D collidedObject)
	{
		if (collidedObject.gameObject.tag == "Wall") {
			pos = old;
		}

		if (collidedObject.gameObject.tag == "Enemy") {
			pos = old;
		}
		if (collidedObject.gameObject.tag == "Player") {
			pos = old;
		}
	}
}
