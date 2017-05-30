using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTower : TowerDecorator {

    public SupportTower(TowerCore towerCore) : base(towerCore)
    {
        
    }

    public override void ChangeCore()
    {
        base.ChangeCore();
    }

    public override void Ability()
    {
        base.Ability();
    }
}
