using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
	public Text timeText;
	public Text stepsText;
	public Text collectibleText;

	public Text finalTimeText, remainingStepsText, finalCollectibleText;

	public float startTime = 300.0f; // Time given to complete game
	float timeRemaining;
	public int collectibleCount = 0;

	public static CanvasScript Instance;

    void Awake()
    {
		Instance = this;
    }
    void Start()
	{
		timeText.material.color = Color.white; // GUI text color
	}

	void Update()
	{
		CountDown();
		ShowSteps();
		UpdateCollectibleText();
		DisplayFinalTimeGrade();
		ShowFinalRemainingSteps();
	}
	//displays your final grade based on the time
	void DisplayFinalTimeGrade()
    {
		string grade;
        if (timeRemaining < startTime && !(timeRemaining < (0.8 * startTime)))
        {
			grade = "A";
			finalTimeText.text = "Time Grade: " + grade;
		}	
		else if (timeRemaining < (0.8 * startTime) && !(timeRemaining < (0.6 * startTime)))
        {
			grade = "B";
			finalTimeText.text = "Time Grade: " + grade;
		}
		else if (timeRemaining < (0.6 * startTime) && !(timeRemaining < (0.4 * startTime)))
        {
			grade = "C";
			finalTimeText.text = "Time Grade: " + grade;
		}
		else if (timeRemaining < (0.4 * startTime) && !(timeRemaining < (0.2 * startTime)))
        {
			grade = "D";
			finalTimeText.text = "Time Grade: " + grade;
		}
		else if (timeRemaining < (0.2 * startTime) && !(timeRemaining == 0))
        {
			grade = "E";
			finalTimeText.text = "Time Grade: " + grade;
		}
		else if (timeRemaining == 0)
        {
			grade = "F";
			finalTimeText.text = "Time Grade: " + grade;
		}
    }
	//shows final remaining steps
	void ShowFinalRemainingSteps()
    {
		remainingStepsText.text = "Steps Left: " + CubeRoll.Instance.steps.ToString();
    }
	//updates the collectibles text
	public void UpdateCollectibleText()
    {
		collectibleText.text = "Collectible: " + (PlayerPrefs.GetInt("collectible") + collectibleCount);
		finalCollectibleText.text = "Collectibles: " + (PlayerPrefs.GetInt("collectible") + collectibleCount);
	}
	//counts how much collectibles player has picked up and updates the playerprefs
	public void CountCollectibles(int value)
    {
		string storedCollectible = "collectible";
		collectibleCount += value;
		PlayerPrefs.SetInt(storedCollectible, collectibleCount);
    }
	//shows how many steps left
	void ShowSteps()
    {
		stepsText.text = "STEPS LEFT: " + CubeRoll.Instance.steps.ToString();
		if(CubeRoll.Instance.steps <= 0)
        {
			Lose();
        }
	}
	//counts down the timer
	void CountDown()
	{
		timeRemaining = startTime - Time.timeSinceLevelLoad;
		ShowTime();

		if (timeRemaining < 0)
		{
			timeRemaining = 0;
			Lose();
		}
	}
	//displays the time
	void ShowTime()
	{
		int minutes;
		int seconds;
		string timeString;

		minutes = (int)timeRemaining / 60; // Derive minutes by dividing seconds by 60 seconds
		seconds = (int)timeRemaining % 60; // Derive remainder after dividing by 60 seconds
		timeString = "Time Left: " + minutes.ToString() + ":" + seconds.ToString("d2");
		timeText.text = timeString;
	}
	//load the lose scene
	void Lose()
	{
		SceneManager.LoadScene("Lose");
	}
}
