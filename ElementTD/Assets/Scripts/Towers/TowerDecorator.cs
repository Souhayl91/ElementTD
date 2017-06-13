using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDecorator : TowerCore
{
    protected TowerCore _towerCore;

    public TowerDecorator(TowerCore towerCore)
    {
        _towerCore = towerCore;
    }

    public override void ChangeCore()
    {
        _towerCore.ChangeCore();
    }

    public override void Ability()
    {
       _towerCore.Ability();
    }
}
