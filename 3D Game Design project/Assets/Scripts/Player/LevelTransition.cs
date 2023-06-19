using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string nextLevel;
    public string mainMenu;

    public GameObject instructionsScreen;

    //loads the level set by the words
    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
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
}
