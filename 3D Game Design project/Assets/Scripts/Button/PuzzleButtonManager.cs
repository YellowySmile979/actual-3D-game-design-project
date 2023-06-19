using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtonManager : MonoBehaviour
{
    public static PuzzleButtonManager Instance;

    public List<PuzzleButton> puzzleButtons = new List<PuzzleButton>();
    public int counter;
    GameObject thePuzzleButton;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        CheckForMoreThanTwoActivations();
    }
    //checks to see if more than two buttons have been activated
    public void CheckForMoreThanTwoActivations()
    {
        if (counter >= 2)
        {
            //finds the button that has been affected and updates the private GameObject
            PuzzleButton chosenPuzzleButton = puzzleButtons.Find(s => s.hasBeenAffected == false);
            //sets that one to have it's trigger set to false
            chosenPuzzleButton.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            thePuzzleButton = chosenPuzzleButton.gameObject;
        }
        if (counter < 2 && thePuzzleButton != null)
        {
            thePuzzleButton.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
