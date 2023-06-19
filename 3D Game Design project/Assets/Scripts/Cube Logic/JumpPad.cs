using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Transform destination;
    public float speed = 7f;
    public bool disableDestinationJumpPad = true;
    public float waitTime = 0.7f;
    float pushBlockWaitTime;
    public Vector3 pushBlockPushDirection;
    GameObject pushBlock;

    //all the objects that we do not want to launch
    public List<Transform> noFlyList = new List<Transform>();

    void Start()
    {
        //sets the waitTime to always be greater than 0.7f
        //otherwise the pushblock would wait too short of a time and bug out
        if(waitTime < 0.7f)
        {
            waitTime = 0.7f;           
        }
        pushBlockWaitTime = waitTime;
    }
    public void NotifyNoFly(Transform t)
    {
        noFlyList.Add(t);
    }
    void OnTriggerEnter(Collider other)
    {
        //if we r on no fly list, then we cannot be launched
        //which prevents us from getting launched when landing on a jump pad
        if (noFlyList.Contains(other.transform))
        {
            noFlyList.Remove(other.transform);
            return;
        }
        //begin launch
        if (other.GetComponent<CubeRoll>() || other.GetComponent<PushBlock>())
        {
            //checks to see if the collided block is a PushBlock
            //if so, update the GameObject pushBlock with the info and set the waitTime to the one for pushBlock
            //otherwise set it to zero to be instantaneous
            //u could make this a switch case if u want
            if (other.GetComponent<PushBlock>())
            {
                pushBlock = other.gameObject;
                waitTime = pushBlockWaitTime;
            }
            else if (other.GetComponent<CubeRoll>())
            {
                waitTime = 0f;
            }
            StartCoroutine(Jump(other.transform));            
        }
    }
    //controls the jumping part of the jumppad
    IEnumerator Jump(Transform launchTarget)
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();

        //waits so that the pushblock doesnt bug out
        yield return new WaitForSeconds(waitTime);

        //notifies the landing jump pad that it should not allow incoming object to be launched again
        JumpPad jumpPad = destination.GetComponent<JumpPad>();
        if (jumpPad) jumpPad.NotifyNoFly(launchTarget);

        //before we jump, check if the object has a rigidbody. if it does, set it to Kinematic
        Rigidbody rb = launchTarget.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        //record some of our key variables
        float dist = 0f, totalDist = Vector3.Distance(launchTarget.position, destination.position + Vector3.up);

        //where the launch target will actually be
        Vector3 actualPosition = launchTarget.position;

        //"Update" loop
        while (dist < totalDist)
        {
            float delta = speed * Time.deltaTime;
            actualPosition = Vector3.MoveTowards(actualPosition, 
                destination.position + Vector3.up, delta);
            dist += delta;

            //move launch target to the actual position + vertical offset
            float offsetHeight = totalDist * Mathf.Sin(Mathf.PI * dist / totalDist);
            launchTarget.position = actualPosition + Vector3.up * offsetHeight;

            yield return w;
        }
        launchTarget.position = destination.position + Vector3.up;
        //causes pushblock to be pushed in a direction so that the push block aint just occupying the jumppad
        if (pushBlock != null)
        {
            launchTarget.position = destination.position + pushBlockPushDirection;
            //sets the GameObject to null for the next block to be detected
            pushBlock = null;
        }
        //disable kinematic for the rb again
        if (rb) rb.isKinematic = false;
    }
}
