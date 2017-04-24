using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemScript : MonoBehaviour {

	public GameObject itemPicture;
	public GameObject itemName;
	public GameObject itemPrice;
	public Sprite Default;
	public Sprite PinkandBlue;
	public Sprite Mozaic;
	public Sprite Deer;

	public void setItem(string name, int price) {
		Sprite selectedSprite;
		if (name.Equals ("Default")) {
			selectedSprite = Default;
		} else if (name.Equals ("Pink and Blue")) {
			selectedSprite = PinkandBlue;
		} else if (name.Equals ("Mozaic")) {
			selectedSprite = Mozaic;
		} else {
			selectedSprite = Deer;
		}
		itemPicture.GetComponent<Image> ().sprite = selectedSprite;
		itemName.GetComponent<Text> ().text = name;
		itemPrice.GetComponent<Text> ().text = price.ToString ();
	}
}
