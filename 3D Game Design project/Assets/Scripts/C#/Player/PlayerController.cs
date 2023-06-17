using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<GameObject> growCubes = new List<GameObject>();
    public GrowCubeData growCubeData, biggerGrowCubeData;
    public int number;

    [SerializeField] int chosenGrowCube;
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
    //resummons the grow cube after the z key is used and the player aint already small
    public void ResummonCube()
    {
        if (Input.GetKeyDown(KeyCode.Z) && playerCube.transform.localScale != new Vector3(1, 1, 1))
        {
            //helps check and spawn the right grow cube
            switch (chosenGrowCube) 
            {
                default:
                    return;
                case 0:
                    //finds the correct growcube within the list and spawns it
                    GameObject growCube = Instantiate(growCubes.Find(
                        s => growCubeData.growthSizeFactor == GrowCubeData.GrowthSizeFactor.size2), 
                        transform.position + new Vector3(0.5f, 0, 0.5f), 
                        Quaternion.identity);
                    //afterwards immediately deleting it
                    growCubes.Remove(growCubes.Find(s => growCube));
                    CubeRoll.Instance.SetScale(1);
                    growCubeData.hasBeenUsed = false;
                    number = 0;
                    break;
                case 1:
                    //same thing as above
                    GameObject biggerGrowCube = Instantiate(growCubes.Find(
                        s => biggerGrowCubeData.growthSizeFactor == GrowCubeData.GrowthSizeFactor.size3),
                        transform.position + new Vector3(-1.5f, -1, -1.5f),
                        Quaternion.identity);
                    growCubes.Remove(growCubes.Find(s => biggerGrowCube));
                    CubeRoll.Instance.SetScale(2);
                    biggerGrowCubeData.hasBeenUsed = false;
                    number = 1;
                    chosenGrowCube--;
                    break;
            }
        }
    }
    //receives and sets the correct index
    public void IndexOfGrowCube(int index)
    {
        chosenGrowCube = index;
    }
    //receives and updates the list with the new growcube info
    public void ReceiveCubeInfo(GameObject growCubeInfo)
    {
        growCubes.Add(growCubeInfo);
    }
}
