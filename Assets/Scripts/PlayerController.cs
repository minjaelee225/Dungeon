using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float speed = 2.0f;
	Vector3 pos;
	Vector3 old;
	Vector3 oold;
	public int level = 1;
	Transform tr;
	private Animator anim;
	private static bool playerExists;
	public GameObject swordObject;
	public GameObject fireballObject;
	int atk;
	int magic;

	//HP
	private int currentHP;
	private int maxHP;

	//Direction facing
	private bool left;
	private bool right;
	private bool up;
	private bool down;

	void Start()
	{
		pos = transform.position;
		tr = transform;
		anim = GetComponent<Animator>();

		currentHP = GetComponent<PlayerHealth> ().MaxHP;
		maxHP = GetComponent<PlayerHealth> ().MaxHP;

		left = false;
		right = false;
		up = false;
		down = true;

		/**if (!playerExists) {
			playerExists = true;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}*/

	}

	void Update()
	{
		if (currentHP > maxHP) {
			currentHP = maxHP;
		}

		if (currentHP <= 0) {
			Die ();
		}

		atk = GetComponent<PlayerStats> ().atk;
		magic = GetComponent<PlayerStats> ().magic;
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.UpArrow) && tr.position == pos)
		{
			anim.SetTrigger ("bwalk");
			anim.ResetTrigger("lwalk");
			anim.ResetTrigger("rwalk");
			anim.ResetTrigger("fwalk");

			oold = old;
			old = pos;

			left = false;
			right = false;
			up = true;
			down = false;
			pos += (Vector3.up) / 1;
	
		}
		else if (Input.GetKey(KeyCode.RightArrow) && tr.position == pos)
		{
			anim.SetTrigger ("rwalk");
			anim.ResetTrigger("bwalk");
			anim.ResetTrigger("lwalk");
			anim.ResetTrigger("fwalk");

			oold = old;
			old = pos;

			left = false;
			right = true;
			up = false;
			down = false;
			pos += (Vector3.right) / 1;
		
		}
		else if (Input.GetKey(KeyCode.DownArrow) && tr.position == pos)
		{
			anim.SetTrigger ("fwalk");
			anim.ResetTrigger("bwalk");
			anim.ResetTrigger("rwalk");
			anim.ResetTrigger("lwalk");

			oold = old;
			old = pos;

			left = false;
			right = false;
			up = false;
			down = true;

			pos += (Vector3.down) / 1;
		
		}
		else if (Input.GetKey(KeyCode.LeftArrow) && tr.position == pos)
		{
			anim.SetTrigger ("lwalk");
			anim.ResetTrigger("bwalk");
			anim.ResetTrigger("rwalk");
			anim.ResetTrigger("fwalk");

			oold = old;
			old = pos;

			left = true;
			right = false;
			up = false;
			down = false;

			pos += (Vector3.left) / 1;
		}

		else if (Input.GetKeyDown(KeyCode.Space))
		{
			/**Debug.Log ("hello");
			Attack attack = new Attack(5, 1, "Melee");
			Collider2D[] hitObjects = Physics2D.OverlapCircleAll (transform.position, 0.75f);
			for (int i = 0; i < hitObjects.Length; i++) {
				if (hitObjects [i].gameObject.tag == "Hitbox") {
					hitObjects [i].gameObject.SendMessage ("TakeDamage", attack, SendMessageOptions.DontRequireReceiver); 
				}
			}*/
			GameObject sword = Instantiate (swordObject);
			if (left) {
				sword.transform.position = pos + Vector3.left;
			} else if (right) {
				sword.transform.position = pos + Vector3.right;
			} else if (up) {
				sword.transform.position = pos + Vector3.up;
			} else if (down) {
				sword.transform.position = pos + Vector3.down;
			}
			sword.GetComponent<SwordScript> ().init (atk);

		}

		else if (Input.GetKeyDown(KeyCode.A))
		{
			GameObject fireball = Instantiate (fireballObject);
			fireball.transform.position = pos;
			if (left) {
				fireball.GetComponent<FireBallScript> ().init (magic, new Vector2(-1, 0));
			} else if (right) {
				fireball.GetComponent<FireBallScript> ().init (magic, new Vector2(1, 0));
			} else if (up) {
				fireball.GetComponent<FireBallScript> ().init (magic, new Vector2(0, 1));
			} else if (down) {
				fireball.GetComponent<FireBallScript> ().init (magic, new Vector2(0, -1));
			}

		}

		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
	}

	void Die() 
	{
		//Restart the game on death
		SceneManager.LoadScene (0);
	}

	void OnCollisionEnter2D(Collision2D collidedObject)
	{
		if (collidedObject.gameObject.tag == "Wall") {
			pos = old;
		}

		if (collidedObject.gameObject.tag == "Enemy") {
			currentHP -= 1;
			pos = old;
		}
	

		/**
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

		if ((int)(x + 0.5) > (int)x && x > 0)
			x = (int)x + 0.5f;
		else if ((int)(x - 0.5) < (int)x && x < 0)
			x = (int)x - 0.5f;
		else
			x = (int)x;

		if ((int)(y + 0.5) > (int)y && y > 0)
			y = (int)y + 0.5f;
		else if ((int)(y - 0.5) < (int)y && y < 0)
			y = (int)y - 0.5f;
		else
			y = (int)y;

		z = 0f;

		Vector3 away = new Vector3(x, y, z);
		pos = away;
		**/
	}

	public struct Attack {

		private int damage;
		private int range;
		private string name;

		public Attack(int damage, int range, string name) {
			this.damage = damage;
			this.range = range;
			this.name = name;
		}

		public int getDamage()
		{
			return damage;
		}

		public int getRange()
		{
			return range;
		}

		public string getName()
		{
			return name;
		}

	}

}