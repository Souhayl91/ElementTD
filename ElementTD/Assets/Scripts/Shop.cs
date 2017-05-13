using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public TowerBuilder towerBuilder;
    public NodeManager nodeManager;

    public void PurchaseWaterTower()
    {
        Debug.Log("Standard tower purchased");
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.waterTower);
        Buy();
    }

    public void PurchaseEarthTower()
    {
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.earthTower);
        Buy();
    }

    public void PurchaseNatureTower()
    {
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.natureTower);
        Buy();
    }
    public void PurchaseFireTower()
    {
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.fireTower);
        Buy();
    }
    public void PurchaseLightTower()
    {
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.lightTower);
        Buy();
    }
    public void PurchaseDarkTower()
    {
        BuildManager.instance.SetTowerToBuild(BuildManager.instance.darkTower);
        Buy();
    }

    void Buy()
    {
       
        if (BuildManager.instance.GetTowerToBuild() == null)
        {
            return;
        }

        if (nodeManager.selectedNode._tower != null)
        {
            Debug.Log("Already a tower placed at this node!");
            return;
        }

        BuildManager.instance.BuildTower(nodeManager.selectedNode.transform.position, nodeManager.selectedNode.transform.rotation);
        nodeManager.selectedNode._tower = BuildManager.instance.tower;

        nodeManager.selectedNode.isClicked = false;
        
    }
}
