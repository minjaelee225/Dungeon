using System.Collections;
using UnityEngine;

 [System.Serializable]
public class Item {

	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public int itemPower;
	public int itemSpeed;
	public ItemType itemType;

	public enum ItemType {
		Consumable,
		Food,
		Weapon,
		Skill

	}

	public Item(string name, int id, string itemDesc, int power, int speed, ItemType type) {
		itemName = name;
		itemID = id;
		itemIcon = Resources.Load<Texture2D> ("item icons/" + name);
		itemPower = power;
		itemSpeed = speed;
		itemType = type;
		
	}

	public Item() {
	}

} 
