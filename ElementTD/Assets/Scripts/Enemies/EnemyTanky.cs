using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTanky : BaseEnemy {

	// Use this for initialization
	public void Start () 
    {
        base.Start();
		SetHealth(_health * 1.5f);
        SetSpeed(_speed * 0.5f);
	}
	
}
