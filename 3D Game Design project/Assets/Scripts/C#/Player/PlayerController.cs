using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject growCube;
    GameObject playerCube;

    public static PlayerController Instance;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerCube = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ResummonCube();
    }

    public void ResummonCube()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerCube.transform.localScale != new Vector3(1, 1, 1))
        {
            Instantiate(growCube, transform.position + new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
            CubeRoll.Instance.SetScale(1);
        }
    }
    public void ReceiveCubeInfo(GrowCubeData growCubeData)
    {
        growCube = growCubeData.thisObject;
    }
}
