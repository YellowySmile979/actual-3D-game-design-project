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
	}

	public void CountCollectibles(int value)
    {
		string storedCollectible = "collectible";
		collectibleCount += value;
		PlayerPrefs.SetInt(storedCollectible, collectibleCount);
    }

	void ShowSteps()
    {
		stepsText.text = "STEPS LEFT: " + CubeRoll.steps.ToString();
		if(CubeRoll.steps <= 0)
        {
			Lose();
        }
	}
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

	void Lose()
	{
		SceneManager.LoadScene("Lose");
	}
}
