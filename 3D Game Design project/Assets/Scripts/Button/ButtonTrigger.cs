using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [Header("Lists of objects")]
    public List<BlockAffectedByButton> objectsAffected = new List<BlockAffectedByButton>();
    public List<GameObject> platformBlocks = new List<GameObject>();

    [Header("Boolean Check")]
    [SerializeField] bool activateYesOrNo;

    [Header("SFX")]
    public AudioClip gameButtonSFX;

    // Start is called before the first frame update
    void Start()
    {
        if (objectsAffected == null) objectsAffected.AddRange(FindObjectsOfType<BlockAffectedByButton>());
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            SFXManager.Instance.audioSource.PlayOneShot(gameButtonSFX);
        }

        if (other.GetComponent<PlayerController>())
        {
            //a bool flipflop
            activateYesOrNo = !activateYesOrNo;
            //turns it on or off depending on the bool
            //ForEach basically selects each of the delegates within the list which correspond
            //to the type and perform whatever we want
            if (activateYesOrNo)
            {
                objectsAffected.ForEach(delegate (BlockAffectedByButton blockAffectedByButton)
                {
                    blockAffectedByButton.gameObject.SetActive(false);
                });
            }
            else
            {
                objectsAffected.ForEach(delegate (BlockAffectedByButton blockAffectedByButton)
                {
                    blockAffectedByButton.gameObject.SetActive(true);
                });
            }
            if(platformBlocks != null)
            {
                //handles activation of platform blocks
                //NOTE: once platform is on, it REMAINS ON
                platformBlocks.ForEach(delegate (GameObject gameObject)
                {
                    gameObject.SetActive(true);
                });
            }
        }
    }
}
