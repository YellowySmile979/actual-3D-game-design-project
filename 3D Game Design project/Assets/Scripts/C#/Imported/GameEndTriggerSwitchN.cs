using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour {
	IEnumerator OnTriggerEnter(Collider collider) {
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("Win");
	}
}