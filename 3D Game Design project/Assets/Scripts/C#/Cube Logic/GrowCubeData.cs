using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrowCubeData", menuName = "GrowCubeData/ScriptableGrowData")]
public class GrowCubeData : ScriptableObject
{
    public GameObject thisObject;
    public bool hasBeenUsed = false;
    public float scaleFactor = 2f;
}
