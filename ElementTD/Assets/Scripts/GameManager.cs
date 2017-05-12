using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Data data;
    private WaveManager _waveManager;
    [SerializeField] private int _startingGold = 100;
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
        data = new Data();
        data.SetGold(_startingGold);
        data.SetHP(_startingHP);

        
    }

    public void IncreaseGameSpeed()
    {
        if (gameSpeed < 1f)
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
        if (gameSpeed > 1f)
        {
            gameSpeed--;
        }
        else if (gameSpeed > 0f)
        {
            gameSpeed -= 0.5f;
        }
        else
        {
            Debug.Log("GameSpeed is 0 already");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
