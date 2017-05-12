using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    private int _gold;
    private int _hp;

    public void SetGold(int gold)
    {
        _gold = gold;
    }

    public int GetGold()
    {
        return _gold;
    }

    public bool DecreaseGold(int amount)
    {
        if (amount <= _gold)
        {
            _gold -= amount;
            return true;
        }

        return false;
    }

    public void IncreaseGold(int amount)
    {
        if ((_gold += amount) >= 10000)
            _gold = 9999;
    }

    public void SetHP(int hp)
    {
        _hp = hp;
    }

    public int GetHP()
    {
        return _hp;
    }

    public void DecreaseHP(int amount)
    {
        _hp -= amount;
        if (_hp <= 0)
        {
            Debug.Log("YOU LOSE");
        }
    }

    public void IncreaseHP(int amount)
    {
        _hp += amount;
    }
}
