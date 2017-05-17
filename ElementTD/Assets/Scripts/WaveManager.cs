using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    private EnemyFactory _enemyFactory;

    private const int _enemiesAmount = 5;
    private const float enemyInterval = 0.5f;
    private int _waveCount = 0;

    //Timers
    private float _buildInterval;
    //public float waveTimer = 20f;
    //private float _countdown;
	// Use this for initialization
	public void StartWaveCoroutine ()
	{
	    _buildInterval = 5f;
        //_countdown = 2f;

	    _enemyFactory = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();
        _enemyFactory.SetSpawnPosition();
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(_buildInterval / GameManager.instance.gameSpeed);
            _waveCount++;
            Debug.Log("Wave number: " + _waveCount);
            _enemyFactory.SetWave(_waveCount);

            foreach (BaseEnemy.Gene gene in GameManager.instance.genetics.genes)
            {
                _enemyFactory.SpawnEnemy(gene);
                yield return new WaitForSeconds(enemyInterval / GameManager.instance.gameSpeed);
            }
            //for (int i = 0; i < _enemiesAmount; i++)
            //{
            //    _enemyFactory.SpawnEnemy();
            //    yield return new WaitForSeconds(enemyInterval / GameManager.instance.gameSpeed);
            //}
                
            //Debug.Log(_enemyFactory.enemies.Count);
            
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length <= 0);
            GameManager.instance.genetics.CreateNewGeneration();
        }
    }
}
