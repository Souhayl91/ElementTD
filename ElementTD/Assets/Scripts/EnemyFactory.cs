using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{

    public GameObject fireEnemy;
    public GameObject waterEnemy;
    public GameObject natureEnemy;
    public List<GameObject> enemies;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEnemy()
    {
        Instantiate(waterEnemy, Waypoint.points[0].position, Waypoint.points[0].rotation);
    }
}
