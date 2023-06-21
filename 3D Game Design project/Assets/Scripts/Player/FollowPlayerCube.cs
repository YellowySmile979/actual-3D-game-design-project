using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCube : MonoBehaviour
{
    public Transform target;
    public Vector3 whereToStay;

    // Start is called before the first frame update
    void Start()
    {
        //if user hasnt set the target, auto find the player
        if (target == null)
        {
            target = FindObjectOfType<PlayerController>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
    //makes the cube follow the player while maintaining distance
    void FollowPlayer()
    {
        transform.position = target.transform.position + whereToStay;
    }
}
