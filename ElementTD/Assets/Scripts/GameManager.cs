﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Data data;
    public GameObject wayPointsHolder;
    public Waypoint wavePoint;
    public GeneticAlgorithm genetics;
    public WaveManager waveManager;
    public Caretaker careTaker;
    [SerializeField] private int _startingGold;
    [SerializeField] private int _startingHP = 50;

    public float gameSpeed = 1f; // 15 is max

    public static GameManager instance;
    private bool gameStarted;

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
        waveManager = gameObject.AddComponent<WaveManager>();

        careTaker = gameObject.AddComponent<Caretaker>();
    }

    public void Save()
    {
        Memento memento = gameObject.AddComponent<Memento>();

        // Save all relevant states
        /*  TODO: full save state
         *  -towers
         *  -data
         *  -waves
         */
        memento.SetGeneState(GameManager.instance.genetics.Save());

        // Store the memento with all the states in the caretaker
        careTaker.AddMemento(memento);
    }

    public void Restore()
    {
        Memento memento = careTaker.GetLastMemento();

        // Restore all relevent states from the memento
        /*  TODO: full restore state
         *  -towers
         *  -data
         *  -waves
         */
        GameManager.instance.genetics.Restore(memento.GetGeneState());
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
            if (!gameStarted)
            {
                Debug.Log("Starting first wave in 5 seconds.");
                waveManager.StartWaveCoroutine();
                gameStarted = true;
            }
        }
	    if (Input.GetKeyDown(KeyCode.S))
	    {
	        data.SaveData();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Save();
            Debug.Log("Saving...");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restore();
            Debug.Log("Restoring...");
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
