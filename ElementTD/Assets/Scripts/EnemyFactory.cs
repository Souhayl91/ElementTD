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

    public GameObject SpawnEnemy(BaseEnemy.Gene gene)
    {
        normalEnemy = CreateEnemy(normalEnemy, gene);
        return Instantiate(normalEnemy, _spawnPosition, _spawnRotation);
    }

    public GameObject CreateEnemy(GameObject enemy, BaseEnemy.Gene gene)
    {
        float health = 47 + _waveCount * (10 + (_waveCount / 7) * 2);
        int gold = 5 + _waveCount / 5;
        enemy.GetComponent<EnemyNormal>().SetStats(health, gold, gene.waterResistance, gene.fireResistance, gene.natureResistance);
        enemy.GetComponent<EnemyNormal>().SetColor();

        return enemy;
    }

    public void SetWave(int wavecount)
    {
        _waveCount = wavecount;
    }
}
