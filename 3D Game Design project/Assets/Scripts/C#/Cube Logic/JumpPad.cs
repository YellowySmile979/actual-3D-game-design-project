using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Transform destination;
    public float speed = 7f;
    public bool disableDestinationJumpPad = true;

    //all the objects that we do not want to launch
    List<Transform> noFlyList = new List<Transform>();

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
            StartCoroutine(Jump(other.transform));
        }
    }
    IEnumerator Jump(Transform launchTarget)
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();

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
        //disable kinematic for the rb again
        if (rb) rb.isKinematic = false;
    }
}
