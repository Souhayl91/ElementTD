using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyFactory : MonoBehaviour, IFactory
{

    public GameObject normalEnemy;
    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;
    private int _waveCount;

    public void SetSpawnPosition()
    {
        _spawnPosition = GameManager.instance.wavePoint.points[0].position;
        _spawnRotation = GameManager.instance.wavePoint.points[0].rotation;
    }

    public GameObject Spawn(BaseEnemy.Gene gene)
    {
        normalEnemy = Create(normalEnemy, gene);
        return Instantiate(normalEnemy, _spawnPosition, _spawnRotation);
    }

    public GameObject Create(GameObject enemy, BaseEnemy.Gene gene)
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
