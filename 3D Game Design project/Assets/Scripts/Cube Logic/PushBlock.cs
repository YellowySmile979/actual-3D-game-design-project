using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour 
{
	private Vector3 targetPosition;
	private bool isMoving = false;
	private float yOrigin;

	void Start() 
	{
		targetPosition = transform.position;
		yOrigin = transform.position.y;
	}

	void Update() 
	{
		Vector2 pos = new Vector2(transform.position.x, transform.position.z),
			targ = new Vector2(targetPosition.x, targetPosition.z),
			dist = pos - targ;

		if (dist.sqrMagnitude < 0.0001f)
		{
			isMoving = false;
		}
		else if (isMoving)
		{
			// Fix position
			transform.position = Vector3.Lerp(transform.position, targetPosition, 8 * Time.deltaTime);
		}
	}
	//checks to see if the block can move in the direction the player is moving to push the block
	public void Move(Vector3 direction, int distance) 
	{
		RaycastHit hit;
		Physics.Linecast(transform.position, transform.position + direction * -1, out hit);
		Debug.DrawLine(transform.position, transform.position + direction * -1, Color.red);

		if (hit.collider != null && !hit.collider.isTrigger) {
			Debug.Log(hit.collider.name);
		} 
		else 
		{
			transform.position = new Vector3(Mathf.Ceil(transform.position.x) - 0.5f, 
				yOrigin, Mathf.Ceil(transform.position.z) - 0.5f);
			targetPosition = transform.position + (direction * -distance);
			isMoving = true;
			targetPosition = new Vector3(targetPosition.x, yOrigin, targetPosition.z);
		}
	}
}