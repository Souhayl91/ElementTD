using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private GameObject _towerToBuild;

    public GameObject standardTurretPrefab;
    //TODO: list of towers instead of multiple, separated game objects.
    public GameObject waterTower;
    public GameObject earthTower;
    public GameObject natureTower;
    public GameObject fireTower;
    public GameObject darkTower;
    public GameObject lightTower;
    public GameObject tower;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one build manager");
            return;
        }
        instance = this;
    }
	// Use this for initialization
	void Start ()
	{
	    _towerToBuild = standardTurretPrefab;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetTowerToBuild()
    {
        return _towerToBuild;
    }

    public void SetTowerToBuild(GameObject tower)
    {
        _towerToBuild = tower;
    }

    public void BuildTower( Vector3 position, Quaternion rotation)
    {
        GameObject towerToBuild = GetTowerToBuild();
        tower = Instantiate(towerToBuild, position, rotation);
    }
}
