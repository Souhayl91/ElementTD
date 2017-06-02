using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeedy : BaseEnemy {

	// Use this for initialization
	public void Start () 
	{
	    base.Start();
        SetSpeed(_speed * 1.5f);
        SetHealth(_maxHealth * 0.5f);
	}
	
}
