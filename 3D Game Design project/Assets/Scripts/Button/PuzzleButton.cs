using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    [Header("List for objects")]
    public List<GameObject> blocksAffected = new List<GameObject>();
    public List<GameObject> buttons = new List<GameObject>();

    [Header("Boolean Checks")]
    public bool hasBeenAffected;
    public bool isResetButton = false;
    public bool isPlatformConnector;

    [Header("SFX")]
    public AudioClip gameButtonSFX;
    [Range(0, 1)]
    public float volumeOfSFX;

    [Header("Info Text")]
    public GameObject infoText;

    //decides if the block should turn off or on
    void DecideToOnOrOffObject()
    {
        if (!isResetButton)
        {
            //you have seen this quite often, so this comment is for all of this
            //ForEach() from a list is like foreach()
            //essentially for all of this type, perform function
            //do take note that this is written as <list>.ForEach(delegate (<type> var) {<ur code>});
            blocksAffected.ForEach(delegate (GameObject gameObject)
            {
                if (gameObject.activeSelf == true)
                {
                    gameObject.SetActive(false);
                }
                else if (gameObject.activeSelf == false)
                {
                    gameObject.SetActive(true);
                }
            });
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        //plays the music
        if (other.GetComponent<PlayerController>())
        {
            SFXManager.Instance.audioSource.PlayOneShot(gameButtonSFX, volumeOfSFX);
        }

        //checks to see if the player is collided and if the button is not a reset button
        if (other.GetComponent<PlayerController>() && !isResetButton)
        {
            //decides if the object should be turned on or off
            DecideToOnOrOffObject();
            //checks to see if the button has been used, and updates the counter accordingly
            if (!hasBeenAffected)
            {
                PuzzleButtonManager.Instance.counter++;
            }
            else
            {
                PuzzleButtonManager.Instance.counter--;
            }
            hasBeenAffected = !hasBeenAffected;
        }
        //same as above but instead checks to see if the button is a reset button
        if (other.GetComponent<PlayerController>() && isResetButton)
        {
            blocksAffected.ForEach(delegate (GameObject gameObject)
            {
                if(gameObject.activeSelf == false)
                {
                    gameObject.SetActive(true);
                }
            });
            PuzzleButtonManager.Instance.counter = 0;
            PuzzleButtonManager.Instance.puzzleButtons.ForEach(delegate (PuzzleButton puzzleButton)
            {
                puzzleButton.hasBeenAffected = false;
            });
        }
        //this is for the platform in between both sides
        //this also deactivates the buttons
        if (other.GetComponent<PlayerController>() && isPlatformConnector)
        {
            blocksAffected.ForEach(delegate (GameObject gameObject)
            {
                gameObject.SetActive(true);
            });
            buttons.ForEach(delegate (GameObject gameObject)
            {
                gameObject.SetActive(false);
            });
            infoText.SetActive(false);
        }
    }
}
