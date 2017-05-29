using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

public class Data : MonoBehaviour
{
    private int _generationIndex;
    private int _generationListIndex;
    private int _gold;
    private int _hp;

    private List<string[]> _rowData;
    private List<BaseEnemy.Gene> _allGenes;
    private List<BaseEnemy.Gene> _fitestGenes;
    private List<BaseEnemy.Gene> _secondFitestGenes;

    private List<GameObject> _allTowers;

    private GameObject goldObject;
    private Text _goldText;
    private GameObject _playerHPObject;
    private Text _playerHPText;

    public Data(int hp, int gold, int generationIndex, int generationListIndex)
    {
        this._hp = hp;
        this._gold = gold;
        this._generationIndex = generationIndex;
        this._generationListIndex = generationListIndex;

        goldObject = GameObject.Find("Gold");
        _goldText = goldObject.GetComponent<Text>();

        _playerHPObject = GameObject.Find("PlayerHP");
        _playerHPText = _playerHPObject.GetComponent<Text>();

        _rowData = new List<string[]>();
        _allGenes = new List<BaseEnemy.Gene>();
        _fitestGenes = new List<BaseEnemy.Gene>();
        _secondFitestGenes = new List<BaseEnemy.Gene>();

        _allTowers = new List<GameObject>();
    }

    public void SetGoldText(string text)
    {
        _goldText.text = text;
    }

    public void SetHPText(string text)
    {
        _playerHPText.text = text;
    }

    public Text GetGoldText()
    {
        return _goldText;
    }

    public Text GetHPText()
    {
        return _playerHPText;
    }

    public void SetPlayerHPText()
    {
        _playerHPObject = GameObject.Find("PlayerHP");
        _playerHPText = _playerHPObject.GetComponent<Text>();
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

    public List<GameObject> GetAllTowers()
    {
        return _allTowers;
    }
    public List<BaseEnemy.Gene> GetAllGenes()
    {
        return _allGenes;
    }

    public List<BaseEnemy.Gene> GetFitestGenes()
    {
        return _fitestGenes;
    }

    public List<BaseEnemy.Gene> GetSecondFitestGenes()
    {
        return _secondFitestGenes;
    }

    public int GetGenerationIndex()
    {
        return _generationIndex;
    }

    public int GetGenerationListIndex()
    {
        return _generationListIndex;
    }

    
    public void SaveData()
    {

        //Set up the headers of the CSV file
        string[] rowDataTemp = new string[7];
        rowDataTemp[0] = "Generation " + _generationIndex;
        rowDataTemp[1] = "Fire resistance";
        rowDataTemp[2] = "Water resistance";
        rowDataTemp[3] = "Nature resistance";
        rowDataTemp[4] = "Fire tower";
        rowDataTemp[5] = "Water tower";
        rowDataTemp[6] = "Nature tower";

        _rowData.Add(rowDataTemp);

        //Filling the row with the enemy values
        for (int i = 0; i < _allGenes.Count; i++)
        {

            if (i == (_generationListIndex - 9))
            {
                rowDataTemp = new string[5];
                rowDataTemp[1] = _allGenes[i].fireResistance * 100 + " %";
                rowDataTemp[2] = _allGenes[i].waterResistance * 100 + " %";
                rowDataTemp[3] = _allGenes[i].natureResistance * 100 + " %";
            }
            else
            {
                rowDataTemp = new string[4];
                rowDataTemp[1] = _allGenes[i].fireResistance * 100 + " %";
                rowDataTemp[2] = _allGenes[i].waterResistance * 100 + " %";
                rowDataTemp[3] = _allGenes[i].natureResistance * 100 + " %";
            }
            _rowData.Add(rowDataTemp);
            if (i == _generationListIndex)
            {
                //Empty row every new generation
                string[] splitGenerationRowDataTemp = new string[4];
                splitGenerationRowDataTemp[0] = "";
                splitGenerationRowDataTemp[1] = "";
                splitGenerationRowDataTemp[2] = "";
                splitGenerationRowDataTemp[3] = "";
                _rowData.Add(splitGenerationRowDataTemp);

                string[] fittestRowDataTemp = new string[4];
                fittestRowDataTemp[0] = "Fitest gene";
                fittestRowDataTemp[1] = _fitestGenes[_generationIndex - 1].fireResistance * 100 + " %";
                fittestRowDataTemp[2] = _fitestGenes[_generationIndex - 1].waterResistance * 100 + " %";
                fittestRowDataTemp[3] = _fitestGenes[_generationIndex - 1].natureResistance * 100 + " %";
                _rowData.Add(fittestRowDataTemp);

                string[] secondFitestRowDataTemp = new string[4];
                secondFitestRowDataTemp[0] = "Second fitest gene";
                secondFitestRowDataTemp[1] = _secondFitestGenes[_generationIndex - 1].fireResistance * 100 + " %";
                secondFitestRowDataTemp[2] = _secondFitestGenes[_generationIndex - 1].fireResistance * 100 + " %";
                secondFitestRowDataTemp[3] = _secondFitestGenes[_generationIndex - 1].fireResistance * 100 + " %";
                _rowData.Add(secondFitestRowDataTemp);

                //Empty row every new generation
                string[] emptyRowDataTemp = new string[4];
                emptyRowDataTemp[0] = "";
                emptyRowDataTemp[1] = "";
                emptyRowDataTemp[2] = "";
                emptyRowDataTemp[3] = "";
                _rowData.Add(emptyRowDataTemp);

                //Next generation header
                string[] newRowDataTemp = new string[7];
                newRowDataTemp[0] = "Generation " + (_generationIndex + 1);
                newRowDataTemp[1] = "Fire resistance";
                newRowDataTemp[2] = "Water resistance";
                newRowDataTemp[3] = "Nature resistance";
                newRowDataTemp[4] = "Fire tower";
                newRowDataTemp[5] = "Water tower";
                newRowDataTemp[6] = "Nature tower";
                _rowData.Add(newRowDataTemp);

                _generationIndex++;
                _generationListIndex += 10;
            }
        }

        string[][] output = new string[_rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = _rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = GetPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }


    public DirectoryInfo GetRootPath()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/CSV/");

        return dir;
    }
    // Following method is used to retrive the relative path as device platform
    private string GetPath()
    {
        int index = GetFilesCount(GetRootPath());
        return Application.dataPath + "/CSV/" + "Session_" + index + ".csv";

        //for android:  return Application.persistentDataPath+"data.csv";
        //for iphone: return Application.dataPath +"/"+"data.csv";
    }

    public int GetFilesCount(DirectoryInfo d)
    {
        int i = 0;
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Contains("csv"))
                i++;
        }
        return i;
    }
}
