using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public Transform enemy;

    public float waveTimer;
    private float countdown;
	// Use this for initialization
	void Start ()
	{
	    waveTimer = 5f;
	    countdown = 2f;
	}
	
	// Update is called once per frame
	void Update () {
	    if (countdown <= 0f)
	    {
	        SpawnWave();
	        countdown = waveTimer;
	    }

	    countdown -= Time.deltaTime;
	}

    void SpawnWave()
    {
        //Debug.Log("Wave incoming");
    }
}
