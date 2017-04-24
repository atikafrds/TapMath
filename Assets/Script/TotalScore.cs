using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour {

	public int scoreMemory;
	public TextMesh scoreMemoryText;

    // Use this for initialization
    void Start() {
        if (PlayerPrefs.HasKey("scoreMemory")) {
			scoreMemory = PlayerPrefs.GetInt("scoreMemory");
        } else {
			scoreMemory = 0;
        }
		scoreMemoryText.text = "Your score: " + scoreMemory;
    }
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.HasKey("scoreMemory")) {
			scoreMemory = PlayerPrefs.GetInt("scoreMemory");
        } else {
			scoreMemory = 0;
        }
		scoreMemoryText.text = "Your score: " + scoreMemory;
    }
}
