using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGrowCube : MonoBehaviour
{
    [Header("Grow Cube Info")]
    public int whichOneToUse = 0;
    [Header("Cube Bob")]
    public float cubeBobOffset;
    public float bobSpeed = 5f;
    [Header("Cube Rotate")]
    public float rotateSpeed = 5f;
    public GameObject pivot;
    bool direction;
    protected Vector3 originalPosition;
    protected Quaternion originalRotation;

    void Start()
    {
        //sets the pivot of the growcube automatically assuming user hasnt done it already
        if (pivot == null) pivot = this.gameObject;
        //can also be true since all this does is give the cube a push to start moving
        direction = false;
    }
    void Update()
    {
        BobCube();
        RotateCube();
    }
    void OnTriggerEnter(Collider other)
    {
        //checks to see if either of the growcubes have been used and if the colliding block is the player
        if ((GrowCubeNormal.Instance.growCubeData.hasBeenUsed == false || 
            GrowCubeBigger.Instance.biggerGrowCubeData.hasBeenUsed == false) 
            && other.GetComponent<CubeRoll>())
        {
            //updates the function that check and ensures the right size of the cube is set
            PlayerController.Instance.IndexOfGrowCube(whichOneToUse);
            //decides which instructions to use
            if (whichOneToUse == 0)
            {
                PlayerController.Instance.number = 1;
                //sets the scale of the cube
                CubeRoll.Instance.SetScale(GrowCubeNormal.Instance.growCubeData.scaleFactor);
                //updates the cube info in the PlayerController script
                PlayerController.Instance.ReceiveCubeInfo(GrowCubeNormal.Instance.growCubeData.thisObject);
                GrowCubeNormal.Instance.growCubeData.hasBeenUsed = true;
                Destroy(gameObject);
            }
            else if (whichOneToUse == 1)
            {
                if (PlayerController.Instance.number == 1)
                {
                    CubeRoll.Instance.SetScale(GrowCubeBigger.Instance.biggerGrowCubeData.scaleFactor);
                    PlayerController.Instance.ReceiveCubeInfo(GrowCubeBigger.Instance.biggerGrowCubeData.thisObject);
                    GrowCubeBigger.Instance.biggerGrowCubeData.hasBeenUsed = true;
                    Destroy(gameObject);
                }
            }
        }
    }
    //sets the bool to false to ensure when the player shrinks to not trigger the thing again
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CubeRoll>())
        {
            if (whichOneToUse == 0)
            {
                GrowCubeNormal.Instance.growCubeData.hasBeenUsed = false;
            }
            else if (whichOneToUse == 1)
            {
                GrowCubeBigger.Instance.biggerGrowCubeData.hasBeenUsed = false;
            }
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
