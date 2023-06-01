using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public float maxDistance = 20f;
    Transform lastHitMirror;

    LineRenderer lr;

    const int MAX_BOUNCES = 10;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.positionCount = 2;
        Vector3 direction = transform.forward,
                a = transform.position, 
                b = a + direction * maxDistance;

        int bounces = 1;

        lr.SetPosition(0, a);

        while (bounces < lr.positionCount)
        {
            RaycastHit[] hits = Physics.RaycastAll(a, direction, maxDistance);
            foreach (RaycastHit hit in hits)
            {
                //ignore the raycast if we hit ourselves
                if (hit.collider.gameObject == gameObject) continue;
                if (hit.transform == lastHitMirror) continue;

                //otherwise stop the laser's position
                b = hit.transform.position;
                if (hit.transform.CompareTag("Mirror"))
                {
                    direction = hit.transform.right;
                    lr.positionCount++;
                    lastHitMirror = hit.transform;
                }

                break; //we only want the 1st hit
            }
            lr.SetPosition(bounces, b);
            bounces++;
            a = b;

            //failsafe in case of infinite loop
            if (bounces > MAX_BOUNCES) break;
        }        
    }
}
