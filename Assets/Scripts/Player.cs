using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 3;
	protected Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 playerDirection = new Vector2();
		if (Input.GetKey(KeyCode.RightArrow))
		{
			anim.ResetTrigger("bwalk");
			anim.ResetTrigger("lwalk");
			anim.ResetTrigger("fwalk");

			anim.SetTrigger ("rwalk");
			playerDirection.x = 1;

		} else if (Input.GetKey(KeyCode.LeftArrow))
		{
			anim.ResetTrigger("bwalk");
			anim.ResetTrigger("rwalk");
			anim.ResetTrigger("fwalk");

			anim.SetTrigger ("lwalk");
			playerDirection.x = -1;
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			anim.ResetTrigger("lwalk");
			anim.ResetTrigger("rwalk");
			anim.ResetTrigger("fwalk");

			anim.SetTrigger ("bwalk");
			playerDirection.y = 1;
		} else if (Input.GetKey(KeyCode.DownArrow))
		{
			anim.ResetTrigger("bwalk");
			anim.ResetTrigger("rwalk");
			anim.ResetTrigger("lwalk");

			anim.SetTrigger ("fwalk");
			playerDirection.y = -1;
		}
		GetComponent<Rigidbody2D>().velocity = playerDirection.normalized * speed;
	}
}
