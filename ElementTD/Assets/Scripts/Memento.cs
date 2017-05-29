using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memento : MonoBehaviour
{
    private List<BaseEnemy.Gene> _genes;

    public void SetGeneState(List<BaseEnemy.Gene> genes)
    {
        _genes = genes;
    }

    public List<BaseEnemy.Gene> GetGeneState()
    {
        return _genes;
    } 
}
