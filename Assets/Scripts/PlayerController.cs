using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public AudioClip footstep;
	public AudioClip fireball;
	public AudioClip sword;
	public AudioClip hurt;


	public float speed = 2.0f;
	Vector3 pos;
	Vector3 old;
	Vector3 oold;
	public int level = 1;
	Transform tr;
	private Animator anim;
	private static bool playerExists;
	public GameObject swordLeft;
	public GameObject swordRight;
	public GameObject swordUp;
	public GameObject swordDown;
	public GameObject fireballLeft;
	public GameObject fireballRight;
	public GameObject fireballUp;
	public GameObject fireballDown;
	int atk;
	int magic;
	int def;

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

	}

	void Update()
	{
		if (currentHP > maxHP) {
			currentHP = maxHP;
		}

		if (currentHP <= 0) {
			Die ();
		}

		atk = FindObjectOfType<PlayerStats>().atk;
		magic = FindObjectOfType<PlayerStats> ().magic;
		def = FindObjectOfType<PlayerStats> ().def;
	}

	void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) && tr.position == pos)
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

			AudioSource.PlayClipAtPoint(footstep, transform.position, 4f);
	
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow) && tr.position == pos)
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

			AudioSource.PlayClipAtPoint(footstep, transform.position, 4f);
		
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) && tr.position == pos)
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

			AudioSource.PlayClipAtPoint(footstep, transform.position, 4f);
		
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow) && tr.position == pos)
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

			AudioSource.PlayClipAtPoint(footstep, transform.position, 4f);
		}

		else if (Input.GetKeyDown(KeyCode.Space))
		{
			if (left) {
				GameObject swordL = Instantiate (swordLeft);
				swordL.transform.position = pos + Vector3.left;
				swordL.GetComponent<SwordScript> ().init (atk);
			} else if (right) {
				GameObject swordR = Instantiate (swordRight);
				swordR.transform.position = pos + Vector3.right;
				swordR.GetComponent<SwordScript> ().init (atk);
			} else if (up) {
				GameObject swordU = Instantiate (swordUp);
				swordU.transform.position = pos + Vector3.up;
				swordU.GetComponent<SwordScript> ().init (atk);
			} else if (down) {
				GameObject swordD = Instantiate (swordDown);
				swordD.transform.position = pos + Vector3.down;
				swordD.GetComponent<SwordScript> ().init (atk);
			}
			AudioSource.PlayClipAtPoint(sword, transform.position, 0.13f);

		}

		else if (Input.GetKeyDown(KeyCode.A))
		{
			if (left) {
				GameObject fireL = Instantiate (fireballLeft);
				enhanceFire (fireL);
				fireL.transform.position = pos;
				fireL.GetComponent<FireBallScript> ().init (magic, new Vector2(-1, 0));
			} else if (right) {
				GameObject fireR = Instantiate (fireballRight);
				enhanceFire (fireR);
				fireR.transform.position = pos;
				fireR.GetComponent<FireBallScript> ().init (magic, new Vector2(1, 0));
			} else if (up) {
				GameObject fireU = Instantiate (fireballUp);
				enhanceFire (fireU);
				fireU.transform.position = pos;
				fireU.GetComponent<FireBallScript> ().init (magic, new Vector2(0, 1));
			} else if (down) {
				GameObject fireD = Instantiate (fireballDown);
				enhanceFire (fireD);
				fireD.transform.position = pos;
				fireD.GetComponent<FireBallScript> ().init (magic, new Vector2(0, -1));
			}
			AudioSource.PlayClipAtPoint(fireball, transform.position, 0.2f);
		}

		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
	}

	void Die() 
	{
		//Restart the game on death
		SceneManager.LoadScene (1);
	
	}

	void enhanceFire(GameObject fire)
	{
		if (magic >= 10 && magic < 20) {
			fire.GetComponent<SpriteRenderer> ().color = Color.black;
		}
		if (magic >= 20 && magic < 35) {
			fire.GetComponent<SpriteRenderer> ().color = Color.magenta;
		}
		if (magic >= 35 && magic < 50) {
			fire.GetComponent<SpriteRenderer> ().color = Color.red;
		}
		if (magic >= 50) {
			fire.GetComponent<SpriteRenderer> ().color = Color.blue;
		}
	}



	void OnCollisionEnter2D(Collision2D collidedObject)
	{
		if (collidedObject.gameObject.tag == "Wall") {
			pos = old;
		}

		if (collidedObject.gameObject.tag == "Enemy") {
			currentHP -= collidedObject.gameObject.GetComponent<Enemy>().attack1Damage;
			pos = old;
			AudioSource.PlayClipAtPoint(hurt, transform.position, 0.3f);
		}
	}

}