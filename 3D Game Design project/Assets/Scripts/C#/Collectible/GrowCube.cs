using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCube : MonoBehaviour
{
    public GrowCubeData growCubeData;

    void OnTriggerEnter(Collider other)
    {
        CubeRoll.Instance.SetScale(2);
        PlayerController.Instance.ReceiveCubeInfo(growCubeData);
        Destroy(gameObject);
    }
}
