using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    
    public void PurchaseWaterTower()
    {
        Debug.Log("Standard tower purchased");
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.waterTower);
    }

    public void PurchaseEarthTower()
    {
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.earthTower);
    }

    public void PurchaseNatureTower()
    {
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.natureTower);
    }
    public void PurchaseFireTower()
    {
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.fireTower);
    }
    public void PurchaseLightTower()
    {
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.lightTower);
    }
    public void PurchaseDarkTower()
    {
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.darkTower);
    }
}
