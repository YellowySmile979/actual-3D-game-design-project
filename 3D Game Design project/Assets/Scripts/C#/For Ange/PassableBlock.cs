using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassableBlock : MonoBehaviour
{
    public bool isPlayer;
    BoxCollider thisBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        thisBoxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCollision();
    }
    //changes the collision depending on the isPlayer bool
    void ChangeCollision()
    {
        if (isPlayer)
        {
            thisBoxCollider.isTrigger = true;
        }
        else
        {
            thisBoxCollider.isTrigger = false;
        }
    }
    //checks to see which block has entered
    //if its the player one, then set isPlayer to  true, otherwise set to false
    //also the OnTriggerStay helps to constantly update this with PushBlock so that player cant enter so long
    //as block remians there
    public void CheckForPlayer(GameObject gameObject)
    {
        if (gameObject.GetComponent<CubeRoll>() || gameObject.tag == "Player")
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }
    }
}
