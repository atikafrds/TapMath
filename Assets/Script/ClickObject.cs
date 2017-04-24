using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour {

	public GameObject objectNumber;
	public static bool isWrong;

	// Use this for initialization
	void Start () {
		if (objectNumber.name == "Text1") {
			objectNumber.GetComponent<TextMesh> ().text = ValueNumber.numberValue1.ToString ();
		} else if (objectNumber.name == "Text2") {
			objectNumber.GetComponent<TextMesh> ().text = ValueNumber.numberValue2.ToString ();
		} else if (objectNumber.name == "Text3") {
			objectNumber.GetComponent<TextMesh> ().text = ValueNumber.numberValue3.ToString ();
		}
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	public void OnMouseDown() {
		if (GameFlow.remainingItems == 3) {
			if (string.Equals(objectNumber.GetComponent<TextMesh> ().text, ValueNumber.valueMin)) {
				SetInvisible ();
				GameFlow.remainingItems--;
			} else {
				Debug.Log ("Wrong");
				GameFlow.life--;
				PlayerPrefs.SetInt ("scoreMemory", GameFlow.score + GameFlow.scoreMemory);
			}
		} else if (GameFlow.remainingItems == 2) {
			if (string.Equals(objectNumber.GetComponent<TextMesh> ().text, ValueNumber.valueMed)) {
				SetInvisible ();
				GameFlow.remainingItems--;
			} else {
				Debug.Log ("Wrong");
				GameFlow.life--;
				PlayerPrefs.SetInt ("scoreMemory", GameFlow.score + GameFlow.scoreMemory);
			}
		} else if (GameFlow.remainingItems == 1) {
			if (string.Equals(objectNumber.GetComponent<TextMesh> ().text, ValueNumber.valueMax)) {
				SetInvisible ();
				GameFlow.remainingItems--;
			} else {
				Debug.Log ("Wrong");
				GameFlow.life--;
				PlayerPrefs.SetInt ("scoreMemory", GameFlow.score + GameFlow.scoreMemory);
			}
		}
	}
		
	//Set mathbox invisible
	void SetInvisible() {
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		objectNumber.GetComponent<TextMesh>().text = "";
	}
		
}
