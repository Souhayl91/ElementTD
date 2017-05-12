using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeedy : BaseEnemy {

	// Use this for initialization
	void Start ()
	{
	    SetSpeed(_speed * 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
