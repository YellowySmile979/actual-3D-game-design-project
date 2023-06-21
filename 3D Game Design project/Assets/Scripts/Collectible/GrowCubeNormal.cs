using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCubeNormal : BaseGrowCube
{
    [Header("Grow Cube Data")]
    public GrowCubeData growCubeData;

    public static GrowCubeNormal Instance;
    //sets original position
    void Awake()
    {
        Instance = this;
        originalPosition = transform.position;
        originalRotation = transform.localRotation;
    }
}
