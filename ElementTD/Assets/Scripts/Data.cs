using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

public class Data : MonoBehaviour
{
    //Data
    public List<string[]> rowData = new List<string[]>();
    public List<BaseEnemy.Gene> allGenes = new List<BaseEnemy.Gene>();
    private int generationIndex = 1;
    private int generationList = 10;
    //

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

    public void SaveData()
    {

        //Set up the headers of the CSV file
        string[] rowDataTemp = new string[4];
        rowDataTemp[0] = "Generation " + generationIndex;
        rowDataTemp[1] = "Fire resistance";
        rowDataTemp[2] = "Water resistance";
        rowDataTemp[3] = "Nature resistance";
        rowData.Add(rowDataTemp);

        //Filling the row with the enemy values
        for (int i = 0; i < allGenes.Count; i++)
        {
            rowDataTemp = new string[4];
            rowDataTemp[1] = allGenes[i].fireResistance * 100 + " %";
            rowDataTemp[2] = allGenes[i].waterResistance * 100 + " %";
            rowDataTemp[3] = allGenes[i].natureResistance * 100 + " %";
            rowData.Add(rowDataTemp);
            if (i == generationList)
            {
                generationIndex++;

                //Empty row every generation
                string[] empyRowDataTemp = new string[4];
                empyRowDataTemp[0] = "";
                empyRowDataTemp[1] = "";
                empyRowDataTemp[2] = "";
                empyRowDataTemp[3] = "";
                rowData.Add(empyRowDataTemp);

                //Next generation header
                string[] newRowDataTemp = new string[4];
                newRowDataTemp[0] = "Generation " + generationIndex;
                newRowDataTemp[1] = "Fire resistance";
                newRowDataTemp[2] = "Water resistance";
                newRowDataTemp[3] = "Nature resistance";
                rowData.Add(newRowDataTemp);

                generationList += 10;
            }
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
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
        return Application.dataPath + "/CSV/" + "Session" + "_" + index + ".csv";

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
