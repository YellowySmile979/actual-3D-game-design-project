using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCube : MonoBehaviour
{
    [Header("Grow Cube Data")]
    public GrowCubeData growCubeData;
    [Header("Cube Bob")]
    public float cubeBobOffset;
    public float bobSpeed = 5f;
    [Header("Cube Rotate")]
    public float rotateSpeed = 5f;
    public GameObject pivot;
    bool direction;
    Vector3 originalPosition;
    Quaternion originalRotation;

    void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.localRotation;
    }
    void Start()
    {
        if (pivot == null) pivot = this.gameObject;
        direction = false;
    }
    void Update()
    {
        BobCube();
        RotateCube();
    }
    void OnTriggerEnter(Collider other)
    {
        if (growCubeData.hasBeenUsed == false && other.GetComponent<CubeRoll>())
        {
            CubeRoll.Instance.SetScale(2);
            PlayerController.Instance.ReceiveCubeInfo(growCubeData);
            growCubeData.hasBeenUsed = true;
            Destroy(gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CubeRoll>())
        {
            growCubeData.hasBeenUsed = false;
        }
    }
    //handles cube bobbing
    void BobCube()
    {
        //if the cube goes to high up, make it go back down and vice versa for too low
        if (transform.position.y >= originalPosition.y + cubeBobOffset - 0.01f)
        {
            direction = true;
        }
        else if (transform.position.y <= originalPosition.y - cubeBobOffset + 0.01f)
        {
            direction = false;
        }
        //depending on the direction of travel, change the direction the cube actual moves
        if (direction)
        {
            /*transform.position = new Vector3(originalPosition.x, 
                                 Mathf.Lerp(transform.position.y, originalPosition.y - cubeBobOffset, bobSpeed),
                                 originalPosition.z
                                 );*/
            transform.position -= new Vector3(0, bobSpeed) * Time.deltaTime;
        }
        else if (!direction)
        {
            /*transform.position = new Vector3(originalPosition.x,
                                 Mathf.Lerp(transform.position.y, originalPosition.y + cubeBobOffset, bobSpeed),
                                 originalPosition.z
                                 );*/
            transform.position += new Vector3(0, bobSpeed) * Time.deltaTime;
        }
    }
    //rotates the cube
    void RotateCube()
    {
        //checks to see if the cube is at the original position or is actively rotating
        //otherwise reset it back to the original rotation
        if(transform.localRotation == originalRotation || transform.localRotation != originalRotation)
        {
            transform.RotateAround(pivot.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
        else if(transform.localRotation == new Quaternion(0, 360, 0, 0))
        {
            transform.localRotation = originalRotation;
        }
    }
}
