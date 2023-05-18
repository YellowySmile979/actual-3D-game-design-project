using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeRoll : MonoBehaviour 
{
	public Transform cubeMesh;
	public bool rollForever = false;
	private float rollSpeed = 400;
	private bool isMoving = false;
	private RaycastHit hit;
	public Vector3 pivot;
	private float cubeSize = 1; // Block cube size
	public static int steps;
	
	//enums are something like classes, this allows for easier access to variables we want to change
	public enum CubeDirection {none, left, up, right, down};
	public CubeDirection direction = CubeDirection.none;

	Quaternion lastRotation;
	
	void Start() 
	{
		//sets the number of steps available
		steps = 500;
		//sets the last rotation
		lastRotation = Quaternion.identity;
	}

	void Update() 
	{
		//checks to see if the cube is moving and in what direction
        if (direction == CubeDirection.none)
        {
			if (Input.GetKeyDown(KeyCode.D)) 
			{
				direction = CubeDirection.right;
			}
			else if (Input.GetKeyDown(KeyCode.A)) 
			{
				direction = CubeDirection.left;
			}
			else if (Input.GetKeyDown(KeyCode.W)) 
			{
				direction = CubeDirection.up;
			}
			else if (Input.GetKeyDown(KeyCode.S)) 
			{
				direction = CubeDirection.down;
			}
		}
		else 
		{
			//this part checks to see if we are currently moving and if we are,
			//flip the cube and then also ensure that the cube can move
			if (!isMoving) 
			{				
				if (CheckCollision(direction)) 
				{
					isMoving = false;
					direction = CubeDirection.none;

					//if push block is in the way, push it
					if (hit.collider.gameObject.GetComponent<PushBlock>())
					{
						hit.collider.gameObject.GetComponent<PushBlock>().Move((transform.position -
							hit.collider.transform.position).normalized, 1);
					}
					return;
				} 
				else 
				{
					CalculatePivot();
					DeductStepCount();
					isMoving = true;
				}
			}

			//handles the rotation of the cube to the new pos
			//after we determine that the cube is able to move
			switch(direction) 
			{
				case CubeDirection.right:
					cubeMesh.transform.RotateAround(pivot, -Vector3.forward, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > 90) ResetPosition();
					break;

				case CubeDirection.left:
					cubeMesh.transform.RotateAround(pivot, Vector3.forward, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > 90) ResetPosition();
					break;

				case CubeDirection.up:
					cubeMesh.transform.RotateAround(pivot, Vector3.right, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > 90) ResetPosition();
					break;

				case CubeDirection.down:
					cubeMesh.transform.RotateAround(pivot, -Vector3.right, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > 90) ResetPosition();
					break;
			}
        }

		if(transform.position.y <= -10) 
		{
			SceneManager.LoadScene("Lose");
		}
	}
	//stops the cube after it has finished moving
	void ResetPosition() 
	{
		//resets the rotation of the cube mesh, and moves the Player Cube object to the pos of the cube mesh
		//finally, it centers the cube mesh back on the player cube
		//cubeMesh.transform.rotation = Quaternion.Euler(Vector3.zero);
		//sets the last rotation
		lastRotation = cubeMesh.transform.rotation = Quaternion.Euler(
				Mathf.Round(cubeMesh.transform.rotation.eulerAngles.x / 90) * 90,
				Mathf.Round(cubeMesh.transform.rotation.eulerAngles.y / 90) * 90,
				Mathf.Round(cubeMesh.transform.rotation.eulerAngles.z / 90) * 90
				);
		transform.position = new Vector3(Mathf.Ceil(cubeMesh.transform.position.x) - 0.5f, 
			transform.position.y, Mathf.Ceil(cubeMesh.transform.position.z) - 0.5f);

		cubeMesh.transform.localPosition = Vector3.zero;
		//is moving is false so the cube doesnt continue moving
		isMoving = false;

		//pushes any push block that is in the direction we move
		/*if(CheckCollision(direction) && hit.collider != null) 
		{
			if(hit.collider.gameObject.GetComponent<PushBlock>()) 
			{
				hit.collider.gameObject.GetComponent<PushBlock>().Move((transform.position - 
					hit.collider.transform.position).normalized, 1);
			}
		}*/

		if (!rollForever)
			direction = CubeDirection.none;
	}

	//flips the cube ie the cube's movement
	void CalculatePivot() 
	{
		//flips the cube correctly in the right direction
		//sets the pivot based on which direction it is moving
		switch(direction) 
		{
			case CubeDirection.right:
				pivot = new Vector3(1, -1, 0);
				break;
			case CubeDirection.left:
				pivot = new Vector3(-1, -1, 0);
				break;
			case CubeDirection.up:
				pivot = new Vector3(0, -1, 1);
				break;
			case CubeDirection.down:
				pivot = new Vector3(0, -1, -1);
				break;
		}

		//calculates the point around which the block will flop
		pivot = transform.position + (pivot * cubeSize * 0.5f);
		if(GetComponent<AudioSource>()) GetComponent<AudioSource>().Play(); // Play the flop sound 
	}

	bool CheckCollision(CubeDirection direction) 
	{
		switch(direction) 
		{
			case CubeDirection.right:
				Physics.Linecast(transform.position, transform.position + transform.right* 1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.right* 1, Color.black);
				break;

			case CubeDirection.left:
				Physics.Linecast(transform.position, transform.position + transform.right* -1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.right* -1, Color.black);
				break;

			case CubeDirection.up:
				Physics.Linecast(transform.position, transform.position + transform.forward* 1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.forward* 1, Color.black);
				break;

			case CubeDirection.down:
				Physics.Linecast(transform.position, transform.position + transform.forward* -1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.forward* -1, Color.black);
				break;
		}

		if(hit.collider == null || (hit.collider != null && hit.collider.isTrigger && !hit.collider.GetComponent("Player"))) 
		{
			return false;
		} 
		else 
		{
			return true;
		}
	}

	void DeductStepCount() 
	{
		steps -= 1;

		if(steps <= 0) 
		{
			steps = 0;
		}
	}
}