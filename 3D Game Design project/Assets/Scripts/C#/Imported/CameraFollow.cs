using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Vector3 camPivot = Vector3.zero;
	public Vector3 camRotation = new Vector3(45, 35, 0);

	public float camSpeed = 5.0f;
	public float camDistance = 5.0f;
	public float camOffset = 0f;
	
	public GameObject target;

	private Vector3 newPos;

	void Update() {
		camPivot = target.transform.position;
		newPos = camPivot;

		transform.eulerAngles = camRotation;
		if(GetComponent<Camera>().orthographic) {
			newPos += -transform.forward * camDistance * 4F;
			GetComponent<Camera>().orthographicSize = camDistance;
		} else
			newPos += -transform.forward * camDistance;
		newPos += transform.right * camOffset;
		transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * camSpeed);
	}
}