using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour 
{
	public float waitTime = 0.2f;
	public GameObject winScreen;
	public GameObject textScreen;

	//detects if player has collided and is small to allow win
	//otherwise show textScreen
	IEnumerator OnTriggerEnter(Collider collider) 
	{
        if (collider.GetComponent<PlayerController>() && CubeRoll.Instance.cubeSize == 1)
        {
			yield return new WaitForSeconds(waitTime);
			Time.timeScale = 0;
			winScreen.SetActive(true);
		}
		if (collider.GetComponent<PlayerController>())
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