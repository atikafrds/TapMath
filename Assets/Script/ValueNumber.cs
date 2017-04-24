using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueNumber : MonoBehaviour {
	public static int numberValue1;
	public static int numberValue2;
	public static int numberValue3;
	public static string valueMax;
	public static string valueMed;
	public static string valueMin;
	public GameObject mathBox1;
	public GameObject mathBox2;
	public GameObject mathBox3;
	public GameObject number1;
	public GameObject number2;
	public GameObject number3;

	void Start() {
		RandomValue (GameFlow.level);
		SetVisible ();
	}

	void Update() {
		if (GameFlow.isLevelUp == true) {
			RandomValue (GameFlow.level);
			SetVisible ();
			GameFlow.isLevelUp = false;
		}
		if ((GameFlow.life == 0) || (GameFlow.timeLeft <= 0)) {
			SetInvisible ();
		}
	}

	//Randomize the value for each objectNumber of MathBox and set the order from the minimum to the maximum
	void RandomValue (int lv) {
		if (lv >= 0 && lv < 10) {
			numberValue1 = Random.Range (0, 100);
			numberValue2 = Random.Range (0, 100);
			numberValue3 = Random.Range (0, 100);
		} else if (lv >= 10 && lv < 20) {
			numberValue1 = Random.Range (-50, 50);
			numberValue2 = Random.Range (-50, 50);
			numberValue3 = Random.Range (-50, 50);
		} else {
			numberValue1 = Random.Range (-100, 100);
			numberValue2 = Random.Range (-100, 100);
			numberValue3 = Random.Range (-100, 100);
		}

		valueMax = (Max (numberValue1, Max (numberValue2, numberValue3))).ToString();
		valueMin = (Min (numberValue1, Min (numberValue2, numberValue3))).ToString();
		valueMed = (numberValue1 + numberValue2 + numberValue3 - int.Parse(valueMax) - int.Parse(valueMin)).ToString();
	}

	// Return the minimum value of A and B
	int Min(int a, int b) {
		if (a >= b) {
			return b;
		} else {
			return a;
		}
	}

	// Return the maximum value of A and B
	int Max(int a, int b) {
		if (a >= b) {
			return a;
		} else {
			return b;
		}
	}

	void SetVisible() {
		mathBox1.GetComponent<SpriteRenderer> ().enabled = true;
		mathBox2.GetComponent<SpriteRenderer> ().enabled = true;
		mathBox3.GetComponent<SpriteRenderer> ().enabled = true;

		number1.GetComponent<TextMesh> ().text = numberValue1.ToString ();
		number2.GetComponent<TextMesh> ().text = numberValue2.ToString ();
		number3.GetComponent<TextMesh> ().text = numberValue3.ToString ();
	}

	void SetInvisible() {
		mathBox1.GetComponent<SpriteRenderer> ().enabled = false;
		mathBox2.GetComponent<SpriteRenderer> ().enabled = false;
		mathBox3.GetComponent<SpriteRenderer> ().enabled = false;

		number1.GetComponent<TextMesh> ().text = "";
		number2.GetComponent<TextMesh> ().text = "";
		number3.GetComponent<TextMesh> ().text = "";
	}
}
