using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [Header("Scene")]
    public string nextLevel;
    public string mainMenu;
    public GameObject instructionsScreen, pauseScreen,
        creditsScreen1, creditsScreen2, creditsScreen3, creditsScreen4, creditsScreen5, creditsScreen6;

    [Header("SFX")]
    public AudioClip buttonSFX;
    [Range(0, 1)]
    public float volumeOfSFX = 0.7f;
    public AudioClip loseSFX, winSFX;
    public bool playLoseSFX = false;
    public bool playWinSFX = false;

    void Start()
    {
        //plays losesfx if bool is ticked
        if (playLoseSFX)
        {
            PlayLoseSFX();
        }
        //plays winsfx if bool is ticked
        if (playWinSFX)
        {
            PlayWinSFX();
        }
    }
    void Update()
    {
        Pause();
    }
    //resumes the level
    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        CameraFollow.Instance.audioSource.UnPause();
    }
    //pauses level
    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CubeRoll.Instance.canMove = false;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            CameraFollow.Instance.audioSource.Pause();
        }
    }
    //plays winSFX
    public void PlayWinSFX()
    {
        SFXManager.Instance.audioSource.PlayOneShot(winSFX, volumeOfSFX);
    }
    //plays the loseSFX
    public void PlayLoseSFX()
    {
        SFXManager.Instance.audioSource.PlayOneShot(loseSFX, volumeOfSFX);
    }
    //loads the level set by the words
    public void NextLevel()
    {
        SFXManager.Instance.audioSource.PlayOneShot(buttonSFX, volumeOfSFX);
        SceneManager.LoadScene(nextLevel);
    }
    //starts the game
    public void StartGame()
    {
        SFXManager.Instance.audioSource.PlayOneShot(buttonSFX, volumeOfSFX);
        PlayerPrefs.SetInt("collectible", 0);
        instructionsScreen.SetActive(true);
    }
    //loads the main menu
    public void BackToMainMenu()
    {
        SFXManager.Instance.audioSource.PlayOneShot(buttonSFX, volumeOfSFX);
        SceneManager.LoadScene(mainMenu);
    }
    //quits the game
    public void Quit()
    {
        SFXManager.Instance.audioSource.PlayOneShot(buttonSFX, volumeOfSFX);
        Application.Quit();
    }
    //shows the instructions
    public void ShowInstructions()
    {
        SFXManager.Instance.audioSource.PlayOneShot(buttonSFX, volumeOfSFX);
        instructionsScreen.SetActive(true);
    }
    //hides the instructions
    public void HideInstructions()
    {
        SFXManager.Instance.audioSource.PlayOneShot(buttonSFX, volumeOfSFX);
        instructionsScreen.SetActive(false);
    }
    int counter = 0;
    //turns on and off the different credits depending on the counter value
    public void ShowCredits()
    {
        SFXManager.Instance.audioSource.PlayOneShot(buttonSFX, volumeOfSFX);
        
        if (counter == 0)
        {
            creditsScreen1.SetActive(true);
            counter++;
        }
        else if (counter == 1)
        {
            creditsScreen2.SetActive(true);
            counter++;
        }
        else if (counter == 2)
        {
            creditsScreen3.SetActive(true);
            counter++;
        }
        else if (counter == 3)
        {
            creditsScreen4.SetActive(true);
            counter++;
        }
        else if (counter == 4)
        {
            creditsScreen5.SetActive(true);
            counter++;
        }
        else if (counter == 5)
        {
            creditsScreen6.SetActive(true);
            counter++;
        }
    }
    //hides the credits
    public void HideCredits()
    {
        SFXManager.Instance.audioSource.PlayOneShot(buttonSFX, volumeOfSFX);
        //resets counter
        counter = 0;
        creditsScreen1.SetActive(false);
        creditsScreen2.SetActive(false);
        creditsScreen3.SetActive(false);
        creditsScreen4.SetActive(false);
        creditsScreen5.SetActive(false);
        creditsScreen6.SetActive(false);
    }
}
