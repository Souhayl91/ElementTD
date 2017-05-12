using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Data _data;
    private WaveManager _waveManager;

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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
