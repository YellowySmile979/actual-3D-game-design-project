using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour 
{
	public string nextScene;
	public float waitTime;

	IEnumerator OnTriggerEnter(Collider collider) 
	{
		yield return new WaitForSeconds(waitTime);
		SceneManager.LoadScene(nextScene);
	}
}