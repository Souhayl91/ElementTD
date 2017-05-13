using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Data data;
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

        _waveManager = gameObject.AddComponent<WaveManager>();
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
        data.goldText.text = "Gold: " + data.GetGold();
	    data.playerHPText.text = "Player HP: " + data.GetHP();
    }
}
