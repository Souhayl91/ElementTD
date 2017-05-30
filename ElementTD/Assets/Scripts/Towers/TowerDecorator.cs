using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDecorator : TowerCore
{
    protected TowerCore _towerCore;
    //public TowerCore DecoratedTowerCore;

    public TowerDecorator(TowerCore towerCore)
    {
        _towerCore = towerCore;
    }

    public override void ChangeCore()
    {
        //if (this.DecoratedTowerCore == null) return;
        //this.DecoratedTowerCore.ChangeCore();
        _towerCore.ChangeCore();
    }

    public override void Ability()
    {
       _towerCore.Ability();
    }
}
