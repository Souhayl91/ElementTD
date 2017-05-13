using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public TowerBuilder towerBuilder;
    public NodeManager nodeManager;
    private int goldCost = 100;
    public void PurchaseWaterTower()
    {
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
        if (nodeManager.selectedNode == null || nodeManager.selectedNode.isClicked == false)
        {
            Debug.Log("Please select a node");
            return;
        }
        if (nodeManager.selectedNode._tower != null)
        {
            Debug.Log("Already a tower placed at this node!");
            return;
        }

        Pay();
    }

    void Pay()
    {
        // TODO remove gold from player 
        if (GameManager.instance.data.DecreaseGold(goldCost))
        {
            BuildManager.instance.BuildTower(nodeManager.selectedNode.transform.position, nodeManager.selectedNode.transform.rotation);
            nodeManager.selectedNode._tower = BuildManager.instance.tower;
            nodeManager.selectedNode.isClicked = false;
        }
        else
        {
            Debug.Log("You need more gold");
        }
        Debug.Log("Current gold: " + GameManager.instance.data.GetGold());

        //if (goldPlayer < goldCost)
        //{
        //    Debug.Log("You need more gold");
        //}
        //else if (goldPlayer > goldCost && nodeManager.selectedNode.isClicked)
        //{
        //    goldPlayer -= goldCost;
        //    BuildManager.instance.BuildTower(nodeManager.selectedNode.transform.position, nodeManager.selectedNode.transform.rotation);
        //    nodeManager.selectedNode._tower = BuildManager.instance.tower;
        //    nodeManager.selectedNode.isClicked = false;
        //}
        //Debug.Log(goldPlayer);
        
        


    }
}
