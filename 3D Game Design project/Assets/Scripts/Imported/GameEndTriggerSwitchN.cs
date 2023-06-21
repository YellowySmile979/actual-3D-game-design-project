using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour 
{
	[Header("Win Objects")]
	public float waitTime = 0.2f;
	public GameObject winScreen;
	public GameObject textScreen;

	[Header("Win SFX")]
	public AudioClip winSFX;
	[Range(0, 1)]
	public float volumeOfSFX;

	//detects if player has collided and is small to allow win
	//otherwise show textScreen
	IEnumerator OnTriggerEnter(Collider collider) 
	{
		//checks to see if player is colliding and if the player is small
        if (collider.GetComponent<PlayerController>() && CubeRoll.Instance.cubeSize == 1)
        {
			//waits a little before doing the following
			yield return new WaitForSeconds(waitTime);
			//"pauses" the game
			CameraFollow.Instance.audioSource.Pause();
			//plays win sfx
			SFXManager.Instance.audioSource.PlayOneShot(winSFX, volumeOfSFX);
			//prevents player from moving
			CubeRoll.Instance.canMove = false;
			//turns on win screen
			winScreen.SetActive(true);
		}
		//if player aint small, turn on text
		if (collider.GetComponent<PlayerController>() && CubeRoll.Instance.cubeSize > 1)
        {
			textScreen.SetActive(true);
        }
	}
	//when cube moves out, turn off text screen
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
			textScreen.SetActive(false);
        }
    }
}