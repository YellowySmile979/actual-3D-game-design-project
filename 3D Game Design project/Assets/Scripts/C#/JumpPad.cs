using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Transform destination;
    public float speed = 7f;

    IEnumerator Jump(Transform launchTarget)
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();

        //record some of our key variables
        float dist = 0f;
        //"Update" loop
        while (Vector2.Distance(
            new Vector2(launchTarget.position.x, launchTarget.position.z), 
            new Vector2(destination.position.x, destination.position.z)
            ) < 0.01f)
        {
            //our "Update" loop
            launchTarget.position = Vector3.MoveTowards(launchTarget.position, destination.position, speed);
            dist += speed;

            yield return w;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
