﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    private EnemyFactory _enemyFactory;

    private const int _enemiesAmount = 5;
    private const float enemyInterval = 0.5f;

    //Timers
    private float _buildInterval;
    public float waveTimer;
    private float _countdown;
	// Use this for initialization
	void Start ()
	{
	    _buildInterval = 1f;
	    waveTimer = 5f;
        _countdown = 2f;

	    _enemyFactory = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();
	    StartCoroutine(Spawner());
	}
	
    IEnumerator Spawner()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(_buildInterval);
            for (int i = 0; i < _enemiesAmount; i++)
            {
                _enemyFactory.SpawnEnemy();
                yield return new WaitForSeconds(enemyInterval);
            }
        }
    }
}
