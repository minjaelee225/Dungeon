using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();

	void Start() {
		items.Add (new Item ("Life Berry", 0, "A yummy berry. How'd it get here and where can I find more? +20 to health", 20, 0, Item.ItemType.Consumable));
	}
}
