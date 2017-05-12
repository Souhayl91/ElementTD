using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Data data;
    private WaveManager _waveManager;
    [SerializeField] private int _startingGold = 100;
    [SerializeField] private int _startingHP = 50;

    public static GameManager instance;
    // Use this for initialization
    void Start ()
    {
        if (instance != null)
        {
            Debug.LogError("More than one game manager");
            return;
        }
        data = new Data();
        data.SetGold(_startingGold);
        data.SetHP(_startingHP);

        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
