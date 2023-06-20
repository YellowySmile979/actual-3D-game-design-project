using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string nextLevel;
    public string mainMenu;

    public GameObject instructionsScreen, creditsScreen1, creditsScreen2, creditsScreen3;

    //loads the level set by the words
    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
    //starts the game
    public void StartGame()
    {
        PlayerPrefs.SetInt("collectible", 0);
        instructionsScreen.SetActive(true);
    }
    //loads the main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
    //quits the game
    public void Quit()
    {
        Application.Quit();
    }
    //shows the instructions
    public void ShowInstructions()
    {
        instructionsScreen.SetActive(true);
    }
    //hides the instructions
    public void HideInstructions()
    {
        instructionsScreen.SetActive(false);
    }
    //turns on and off the different credits depending on which one is on or off
    public void ShowCredits()
    {
        if (creditsScreen1.activeSelf == false && creditsScreen2.activeSelf == false)
        {
            creditsScreen1.SetActive(true);
        }
        else if (creditsScreen1.activeSelf == true)
        {
            creditsScreen1.SetActive(false);
            creditsScreen2.SetActive(true);
        }
        else if (creditsScreen2.activeSelf == true)
        {
            creditsScreen2.SetActive(false);
            creditsScreen3.SetActive(true);
        }
    }
    public void HideCredits()
    {
        creditsScreen1.SetActive(false);
        creditsScreen2.SetActive(false);
        creditsScreen3.SetActive(false);
    }
}
