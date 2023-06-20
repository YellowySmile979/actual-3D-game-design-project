using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour {

    public float spawnEffectTime = 2;
    public float pause = 1;
    public AnimationCurve fadeIn;

    ParticleSystem ps;
    float timer = 0;

	void Start()
    {
        ps = GetComponentInChildren <ParticleSystem>();

        var main = ps.main;
        main.duration = spawnEffectTime;

        ps.Play();

    }
	
	void Update()
    {
        //increases timer until a specific time where upon reaching that time, destroy the object
        if (timer < spawnEffectTime + pause)
        {
            CubeRoll.Instance.canMove = false;
            timer += Time.deltaTime;
        }
        else
        {
            CubeRoll.Instance.canMove = true;
            ps.Play();
            Destroy(gameObject);
        }
    }
}
