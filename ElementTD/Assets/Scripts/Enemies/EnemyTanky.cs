using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTanky : BaseEnemy {

	// Use this for initialization
	void Start ()
    {
		SetHealth(_health * 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
