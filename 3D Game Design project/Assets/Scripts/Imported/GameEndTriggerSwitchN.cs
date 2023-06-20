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
        if (collider.GetComponent<PlayerController>() && CubeRoll.Instance.cubeSize == 1)
        {
			yield return new WaitForSeconds(waitTime);
			CameraFollow.Instance.audioSource.Pause();
			SFXManager.Instance.audioSource.PlayOneShot(winSFX, volumeOfSFX);
			CubeRoll.Instance.canMove = false;
			winScreen.SetActive(true);
		}
		if (collider.GetComponent<PlayerController>() && CubeRoll.Instance.cubeSize > 1)
        {
			textScreen.SetActive(true);
        }
	}
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
			textScreen.SetActive(false);
        }
    }
}