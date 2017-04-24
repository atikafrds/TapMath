using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemScript : MonoBehaviour {

	public GameObject itemName;
	public GameObject itemPrice;
	public GameObject itemPicture;

	public void setItem(string name, int price) {
		itemName.GetComponent<TextMesh> ().text = name;
		itemPrice.GetComponent<TextMesh> ().text = price.ToString ();
		//itemPicture.GetComponent<Image>().sprite = name;
	}
}
