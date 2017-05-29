using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory 
{
    void SetSpawnPosition();
    GameObject Spawn(BaseEnemy.Gene gene);
    GameObject Create(GameObject enemy, BaseEnemy.Gene gene);
}

