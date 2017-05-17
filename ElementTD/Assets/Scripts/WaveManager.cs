﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class WaveManager : MonoBehaviour
{

    private EnemyFactory _enemyFactory;

    public List<GameObject> enemies;
    public List<GameObject> enemiesUI;
    public GameObject enemyElement;
    public GameObject enemyPanel;

    private const int _enemiesAmount = 5;
    private const float enemyInterval = 0.5f;
    private int _waveCount = 0;

    //Timers
    private float _buildInterval;

    //public float waveTimer = 20f;
    //private float _countdown;
	// Use this for initialization
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        enemiesUI = new List<GameObject>();

        enemyPanel = GameObject.Find("EnemyStats");
        enemyElement = Resources.Load("EnemyElement") as GameObject;

        //for (int i = 0; i < enemies.Count; i++)
        //{
        //    GameObject element = Instantiate(enemyElement);
        //    element.transform.parent = enemyPanel.transform;
        //    element.GetComponent<Image>().sprite = enemies[i].GetComponent<SpriteRenderer>().sprite;
        //    element.transform.GetChild(0).GetComponent<Text>().text =
        //        element.GetComponent<BaseEnemy>()._fireResistance +
        //        " " + element.GetComponent<BaseEnemy>()._waterResistance +
        //        " " + element.GetComponent<BaseEnemy>()._natureResistance;
        //}
        
    }
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
            //Debug.Log("Wave number: " + _waveCount);
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
            enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
            enemyPanel = GameObject.Find("EnemyStats");
            enemyElement = Resources.Load("EnemyElement") as GameObject;
            Debug.Log(enemyElement);
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject element = Instantiate(enemyElement);
                
                element.transform.parent = enemyPanel.transform;
                element.GetComponent<Image>().color = enemies[i].GetComponent<SpriteRenderer>().color;

                element.transform.GetChild(0).GetComponent<Text>().text =
                    enemies[i].GetComponent<EnemyNormal>()._fireResistance +
                    " " + enemies[i].GetComponent<EnemyNormal>()._waterResistance +
                    " " + enemies[i].GetComponent<EnemyNormal>()._natureResistance;
                enemiesUI.Add(element);
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length <= 0);
            GameManager.instance.genetics.CreateNewGeneration();
        }
    }
}
