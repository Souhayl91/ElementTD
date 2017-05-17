using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Data data;
    public GameObject wayPointsHolder;
    public Waypoint wavePoint;
    public GeneticAlgorithm genetics;
    private WaveManager _waveManager;
    [SerializeField] private int _startingGold;
    [SerializeField] private int _startingHP = 50;

    public float gameSpeed = 1f; // 15 is max

    public static GameManager instance;
    // Use this for initialization
    void Start ()
    {
        if (instance != null)
        {
            Debug.LogError("More than one game manager");
            return;
        }
        instance = this;
        data = gameObject.AddComponent<Data>();
        data.SetGold(_startingGold);
        data.SetHP(_startingHP);
        data.SetGoldText();
        data.SetPlayerHPText();

        wavePoint = gameObject.AddComponent<Waypoint>();
        wavePoint.SetWayPoints(wayPointsHolder);

        genetics = gameObject.AddComponent<GeneticAlgorithm>();
        genetics.CreateNewRandomPop();

        _waveManager = gameObject.AddComponent<WaveManager>();
        _waveManager.StartWaveCoroutine();
    }

    public void IncreaseGameSpeed()
    {
        if (gameSpeed < 2f)
        {
            gameSpeed += 0.5f;
        }
        else if (gameSpeed < 15f)
        {
            gameSpeed++;
        }
        else
        {
            Debug.Log("GameSpeed is max");
        }
    }

    public void DecreaseGameSpeed()
    {
        if (gameSpeed > 2f)
        {
            gameSpeed--;
        }
        else if (gameSpeed > 0.5f)
        {
            gameSpeed -= 0.5f;
        }
        else
        {
            Debug.Log("GameSpeed is 0.5 already");
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        NewGeneration();
	    }
        data.goldText.text = "Gold: " + data.GetGold();
	    data.playerHPText.text = "Player HP: " + data.GetHP();
    }

    private void NewGeneration()
    {
        //for (int j = 0; j < 20; j++)
        //{
        //    genetics.CreateNewRandomPop();
            for (int i = 0; i < 100; i++)
            {
                genetics.CreateNewGenerationWithOptimal();
                if (genetics.bestFit <= 0.02f)
                {
                    Debug.Log("Found (almost) perfect match! Found it after: " + (i + 1) + " generations.");
                    break;
                }
            }
        //}
    }
}
