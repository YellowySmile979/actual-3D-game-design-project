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
	[HideInInspector] public float cubeSize = 1; // Block cube size
	public int steps;
	public bool canMove = true;

	[Header("Particles")]
	public GameObject walkingParticle;
	public GameObject spawnParticle;
	bool hasSpawnedIn;
	
	//enums are something like classes, this allows for easier access to variables we want to change
	public enum CubeDirection {none, left, up, right, down};
	public CubeDirection direction = CubeDirection.none;

	Quaternion lastRotation;
	bool isClimbing = false;

	public static CubeRoll Instance;

    void Awake()
    {
		Instance = this;
    }
    void Start() 
	{
		canMove = true;
		//sets the number of steps available
		if (steps == 0) steps = 100;
		//sets the last rotation
		lastRotation = Quaternion.identity;
		//PlaySpawnParticle();
	}
	public float GetScale() { return cubeSize; }

	public void SetScale(float size)
	{
		cubeSize = size;
		transform.localScale = new Vector3(size, size, size);
		ResetPosition();
	}
	//plays the spawn particle and destroys it after awhile
	void PlaySpawnParticle()
	{
		float waitTime = 2.8f;
		if (!hasSpawnedIn)
        {
            Instantiate(spawnParticle, transform.position, Quaternion.identity);
			hasSpawnedIn = true;
		}        
		waitTime -= Time.deltaTime;
		if (waitTime <= 0)
        {			
			Destroy(FindObjectOfType<SpawnEffect>().gameObject);
        }
    }
	void Update() 
	{
		if (canMove)
		{
			//if (Input.GetKeyDown(KeyCode.Z)) SetScale(2);
			//else if (Input.GetKeyDown(KeyCode.X)) SetScale(1);
			//if our localScale does not matche the cubeSize set, then we scale our cube towards the cubeSize gradually
			if (Mathf.Abs(cubeSize - transform.localScale.x) > 0.1f)
			{
				transform.localScale = Vector3.Lerp(
					transform.localScale,
					new Vector3(cubeSize, cubeSize, cubeSize),
					Time.deltaTime * 8
					);
			}
			else
			{
				transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
			}
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

						//if push block is in the way, push it
						if (hit.collider.gameObject.GetComponent<PushBlock>())
						{
							hit.collider.gameObject.GetComponent<PushBlock>().Move((transform.position -
								hit.collider.transform.position).normalized, 1);
						}
						else if (!CheckCollision(direction, true))
						{
							CalculatePivot(true);
							DeductStepCount();							
							isMoving = true;
							isClimbing = true;
							return;
						}

						direction = CubeDirection.none;
						return;
					}
					else
					{
						CalculatePivot();
						DeductStepCount();
						Instantiate(walkingParticle, transform.position - new Vector3(0, 0.5f, 0), walkingParticle.transform.localRotation);
						isMoving = true;
					}
				}

				//handles the rotation of the cube to the new pos
				//after we determine that the cube is able to move
				switch (direction)
				{
					case CubeDirection.right:
						cubeMesh.transform.RotateAround(pivot, -Vector3.forward, rollSpeed * Time.deltaTime);
						break;

					case CubeDirection.left:
						cubeMesh.transform.RotateAround(pivot, Vector3.forward, rollSpeed * Time.deltaTime);
						break;

					case CubeDirection.up:
						cubeMesh.transform.RotateAround(pivot, Vector3.right, rollSpeed * Time.deltaTime);
						break;

					case CubeDirection.down:
						cubeMesh.transform.RotateAround(pivot, -Vector3.right, rollSpeed * Time.deltaTime);
						break;
				}
				if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > (isClimbing ? 170 : 90))
				{
					ResetPosition();
				}
			}

			if (transform.position.y <= -10)
			{
				SceneManager.LoadScene("Lose");
			}

			PlaySpawnParticle();
            
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
		//rounding of coords isdifferent for even vs odd
        if (cubeSize % 2 == 0)
        {
			transform.position = new Vector3(
				Mathf.Round(cubeMesh.transform.position.x),
				cubeMesh.transform.position.y,
				Mathf.Round(cubeMesh.transform.position.z)
				);
		}
        else
        {
			transform.position = new Vector3(
				Mathf.Ceil(cubeMesh.transform.position.x) - 0.5f,
				cubeMesh.transform.position.y, 
				Mathf.Ceil(cubeMesh.transform.position.z) - 0.5f
				);
        }

		cubeMesh.transform.localPosition = Vector3.zero;
		//is moving is false so the cube doesnt continue moving
		isMoving = false;
		isClimbing = false;

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
	void CalculatePivot(bool placeOnTop = false) 
	{
		float y = placeOnTop ? 1 : -1;

		//flips the cube correctly in the right direction
		//sets the pivot based on which direction it is moving
		switch(direction) 
		{
			case CubeDirection.right:
				pivot = new Vector3(1, y, 0);
				break;
			case CubeDirection.left:
				pivot = new Vector3(-1, y, 0);
				break;
			case CubeDirection.up:
				pivot = new Vector3(0, y, 1);
				break;
			case CubeDirection.down:
				pivot = new Vector3(0, y, -1);
				break;
		}

		//calculates the point around which the block will flop
		pivot = transform.position + (pivot * cubeSize * 0.5f);
		if(GetComponent<AudioSource>()) GetComponent<AudioSource>().Play(); // Play the flop sound 
	}

	bool CheckCollision(CubeDirection direction, bool checkAbove = false) 
	{
		Vector3 dir = Vector3.zero, offset = checkAbove ? Vector3.up * cubeSize : Vector3.zero;

		switch(direction) 
		{
			case CubeDirection.right:
				dir = transform.right;
				break;

			case CubeDirection.left:
				dir = -transform.right;
				break;

			case CubeDirection.up:
				dir = transform.forward;
				break;

			case CubeDirection.down:
				dir = -transform.forward;
				break;
		}

		//Physics.Linecast(transform.position + offset, transform.position + transform.right* 1, out hit);
		Physics.BoxCast(transform.position + offset, transform.localScale * 0.4f, dir, out hit, Quaternion.identity, cubeSize);
		Debug.DrawLine(transform.position + offset, transform.position + dir * cubeSize, Color.black);

		if (hit.collider == null || (hit.collider != null && hit.collider.isTrigger && !hit.collider.GetComponent("Player"))) 
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