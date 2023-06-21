using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	[Header("Camera Transform")]
	public Vector3 camPivot = Vector3.zero;
	public Vector3 camRotation = new Vector3(45, 35, 0);

	[Header("Camera Variables")]
	public float camSpeed = 5.0f;
	public float camDistance = 5.0f;
	public float camOffset = 0f;
	
	[Header("Target")]
	public GameObject target;

	[Header("Rotate Camera")]
	public float angleOfRotation = 90f;
	public bool doIWantFixedCamera;

	[Header("Audio")]
	public AudioSource audioSource;

	public static CameraFollow Instance;
	Vector3 newPos;

    void Awake()
    {
		Instance = this;
    }
    void Start()
    {
		audioSource = GetComponent<AudioSource>();
    }
    void Update() 
	{
		CameraFollowingPlayer();		
	}
	//makes the camera follows the player
	void CameraFollowingPlayer()
    {
		//sets pivot and the pos of the cam to the pivot
		camPivot = target.transform.position;
		newPos = camPivot;

		if(doIWantFixedCamera) transform.eulerAngles = camRotation;

		//depending on if the cam is orthographic or not, do the following
		if (GetComponent<Camera>().orthographic)
		{
			newPos += -transform.forward * camDistance * 4F;
			GetComponent<Camera>().orthographicSize = camDistance;
		}
		else
		{
			newPos += -transform.forward * camDistance;
		}

		newPos += transform.right * camOffset;
		transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * camSpeed);
	}
}