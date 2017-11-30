using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int MaxHP = 100;
	public int HP;

	// Use this for initialization
	void Start () {
		HP = MaxHP;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void takeDamage(int amount)
	{
		HP -= amount;
		if (HP <= 0) 
		{
			Destroy(gameObject);
			GameObject.FindObjectOfType<PlayerStats> ().atk += 1;
			GameObject.FindObjectOfType<PlayerStats> ().magic += 1;
			GameObject.FindObjectOfType<PlayerStats> ().def += 1;
		}
	}
}
