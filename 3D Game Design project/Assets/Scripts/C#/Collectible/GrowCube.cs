using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCube : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DropPowerUp()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CubeRoll.Instance.SetScale(1);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        CubeRoll.Instance.SetScale(2);
    }
}
