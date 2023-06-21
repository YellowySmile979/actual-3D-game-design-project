using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCubeBigger : BaseGrowCube
{
    [Header("Grow Cube Data")]
    public GrowCubeData biggerGrowCubeData;

    public static GrowCubeBigger Instance;
    //sets original position
    void Awake()
    {
        Instance = this;
        originalPosition = transform.position;
        originalRotation = transform.localRotation;
    }
}
