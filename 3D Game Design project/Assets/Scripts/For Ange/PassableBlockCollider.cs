using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassableBlockCollider : MonoBehaviour
{
    public PassableBlock passableBlock;

    // Start is called before the first frame update
    void Start()
    {
        if(passableBlock == null)
        {
            passableBlock = GetComponentInParent<PassableBlock>();
        }
    }
    //checks to see which block enters the collider and sends the block info to the main script
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PushBlock>() || other.tag == "Pushable")
        {
            passableBlock.CheckForPlayer(other.gameObject);
        }
        else if(other.GetComponent<CubeRoll>() || other.tag == "Player")
        {
            passableBlock.CheckForPlayer(other.gameObject);
        }
    }
    //checks to see if the push block remains in the block
    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PushBlock>() || other.tag == "Pushable")
        {
            passableBlock.CheckForPlayer(other.gameObject);
        }
    }
}
