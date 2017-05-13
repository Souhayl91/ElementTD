using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data : MonoBehaviour
{
    private int _gold;
    private int _hp;
    private GameObject goldObject;
    public Text goldText;
    private GameObject playerHPObject;
    public Text playerHPText;

    public void SetGoldText()
    {
        goldObject = GameObject.Find("Gold");
        goldText = goldObject.GetComponent<Text>();
        
    }

    public void SetPlayerHPText()
    {
        playerHPObject = GameObject.Find("PlayerHP");
        playerHPText = playerHPObject.GetComponent<Text>();
        
    }

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
