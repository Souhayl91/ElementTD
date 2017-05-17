using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{

    //public GameObject fireEnemy;
    public GameObject normalEnemy;
    //public GameObject natureEnemy;
    //public List<BaseEnemy> enemies;

    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;
    private int _waveCount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSpawnPosition()
    {
        _spawnPosition = GameManager.instance.wavePoint.points[0].position;
        _spawnRotation = GameManager.instance.wavePoint.points[0].rotation;
    }

    public void SpawnEnemy(BaseEnemy.Gene gene)
    {
        normalEnemy = CreateEnemy(normalEnemy, gene);
        Instantiate(normalEnemy, _spawnPosition, _spawnRotation);
    }

    public GameObject CreateEnemy(GameObject enemy, BaseEnemy.Gene gene)
    {
        //TODO: Decide health with some formula
        //TODO: Decide gold value by some formula

        //TODO: Set the resistances using genetic algorithm

        float health = 43f + 7f*_waveCount;
        int gold = 5 + (int) (1*_waveCount);
        enemy.GetComponent<EnemyNormal>().SetStats(health, gold, gene.waterResistance, gene.fireResistance, gene.natureResistance);
        //enemy.GetComponent<EnemyNormal>().SetStats(50f, 5, gene.waterResistance, gene.fireResistance, gene.natureResistance);

        return enemy;
    }

    public void SetWave(int wavecount)
    {
        _waveCount = wavecount;
    }
}
