using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyFactory : MonoBehaviour, IFactory
{
    public GameObject normalEnemy;
    public GameObject fastEnemy;
    public GameObject tankyEnemy;

    private GameObject _enemy;
    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;
    private int _waveCount;
    enum EnemyTypes { StandardEnemy, TankEnemy, FastEnemy }

    [SerializeField]
    private EnemyTypes enemyType;

    [SerializeField]
    //private int _goldValue;
    //private int _healthValue;

    public void SetSpawnPosition()
    {
        _spawnPosition = GameManager.instance.wavePoint.points[0].position;
        _spawnRotation = GameManager.instance.wavePoint.points[0].rotation;
    }

    public GameObject Spawn(BaseEnemy.Gene gene)
    {
        _enemy = Create(_enemy, gene);
        return Instantiate(_enemy, _spawnPosition, _spawnRotation);
    }

    public GameObject Create(GameObject enemy, BaseEnemy.Gene gene)
    {
        switch (enemyType)
        {
            case EnemyTypes.StandardEnemy:
            {
                float healthincrease = 47 + _waveCount * (10 + (_waveCount / 7) * 2);
                int goldincrease = 5 + _waveCount / 5;
                enemy = normalEnemy;
                enemy.GetComponent<EnemyNormal>().SetStats(healthincrease, goldincrease, gene.waterResistance, gene.fireResistance, gene.natureResistance);
                enemy.GetComponent<EnemyNormal>().SetColor();
                return enemy;
            }
            case EnemyTypes.FastEnemy:
            {
                float healthincrease = 20 + _waveCount * (10 + (_waveCount / 7) * 2);
                int goldincrease = 5 + _waveCount / 5;
                enemy = fastEnemy;
                enemy.GetComponent<EnemySpeedy>().SetStats(healthincrease, goldincrease, gene.waterResistance, gene.fireResistance, gene.natureResistance);
                enemy.GetComponent<EnemySpeedy>().SetColor();
                return enemy;
            }
            case EnemyTypes.TankEnemy:
            {
                float healthincrease = 60 + _waveCount * (10 + (_waveCount / 7) * 2);
                int goldincrease = 5 + _waveCount / 5;
                enemy = tankyEnemy;
                enemy.GetComponent<EnemyTanky>().SetStats(healthincrease, goldincrease, gene.waterResistance, gene.fireResistance, gene.natureResistance);
                enemy.GetComponent<EnemyTanky>().SetColor();
                return enemy;
            }
            default:
            {
                return enemy;
            }
        }      
    }

    public void RandomType()
    {
        enemyType = (EnemyTypes) Random.Range(0, 3);
    }
    public void SetWave(int wavecount)
    {
        _waveCount = wavecount;
    }

}
