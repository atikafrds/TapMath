using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour {
	public static int timeLeft;
	public static int level = 0;
	public static int remainingItems = 3;
	public static int score;
	public static int scoreMemory;
	public static int life;
	public static bool isLevelUp;
	public GameObject timerText;
	public GameObject scoreText;
	public GameObject gameOverPanel;
	public GameObject endScoreText;

	// Use this for initialization
	void Start () {
		gameOverPanel.SetActive (false);
		timeLeft = 10;
		score = 0;
		life = 1;
		StartCoroutine("RemainingTime");

		if (PlayerPrefs.HasKey ("scoreMemory")) {
			scoreMemory = PlayerPrefs.GetInt ("scoreMemory");
		} else {
			scoreMemory = 0;
		}
		scoreText.GetComponent<Text> ().text = score.ToString();
	}

	// Update is called once per frame
	void Update () {

		timerText.GetComponent<Text>().text = timeLeft.ToString();

		if (timeLeft <= 0) {
			StopCoroutine ("RemainingTime");
			timerText.GetComponent<Text>().text = "Times Up!";
			gameOverPanel.SetActive (true);
			endScoreText.GetComponent<TextMesh> ().text = "Your score: " + score.ToString();
			PlayerPrefs.SetInt ("scoreMemory", score + scoreMemory);
		} else {
			if (life == 0) {
				StopCoroutine ("RemainingTime");
				gameOverPanel.SetActive (true);
				endScoreText.GetComponent<TextMesh> ().text = "Your score: " + score.ToString();
				PlayerPrefs.SetInt ("scoreMemory", score + scoreMemory);
			} else {
				if (remainingItems == 0) {
					StopCoroutine ("RemainingTime");
					isLevelUp = true;
					level++;
					score += 10 * level;
					remainingItems = 3;
					if (level < 20) {
						timeLeft = 10;
					} else {
						timeLeft = 5;
					}

					StartCoroutine ("RemainingTime");
					scoreText.GetComponent<Text> ().text = score.ToString ();
				}
			}
		}
	}

	IEnumerator RemainingTime()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			timeLeft--;
		}
	}
}


