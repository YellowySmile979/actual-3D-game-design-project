using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Related to Grow Cube")]
    public List<GameObject> growCubes = new List<GameObject>();
    public GrowCubeData growCubeData, biggerGrowCubeData;
    public GameObject growCubePrefab, biggerGrowCubePrefab;
    public int number;
    [SerializeField] int chosenGrowCube;

    [Header("SFX")]
    public AudioClip shrinkSFX;
    [Range(0, 1)]
    public float volumeOfSFX;

    GameObject playerCube;    

    //a singleton, makes calling this script easier
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
            SFXManager.Instance.audioSource.PlayOneShot(shrinkSFX, volumeOfSFX);
            //helps check and spawn the right grow cube
            switch (chosenGrowCube) 
            {
                default:
                    return;
                case 0:
                    //spawns the corresponding prefab
                    GameObject growCube = Instantiate(
                        growCubePrefab, 
                        transform.position + new Vector3(0.5f, 0, 0.5f), 
                        Quaternion.identity
                        );
                    //afterwards immediately deleting it by finding the same type within the list
                    growCubes.Remove(growCubes.Find(b => growCube));
                    CubeRoll.Instance.SetScale(1);
                    growCubeData.hasBeenUsed = false;
                    number = 0;
                    break;
                case 1:
                    //same thing as above
                    GameObject biggerGrowCube = Instantiate(
                        biggerGrowCubePrefab,
                        transform.position + new Vector3(-1.5f, -1, -1.5f),
                        Quaternion.identity
                        );
                    growCubes.Remove(growCubes.Find(c => biggerGrowCube));
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
