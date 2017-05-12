using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{

    public GameObject fireEnemy;
    public GameObject waterEnemy;
    public GameObject natureEnemy;
    public List<BaseEnemy> enemies;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEnemy()
    {
        BaseEnemy enemy = new BaseEnemy();
        Instantiate(waterEnemy, Waypoint.points[13].position, Waypoint.points[13].rotation);
        enemies.Add(enemy);
    }
}
