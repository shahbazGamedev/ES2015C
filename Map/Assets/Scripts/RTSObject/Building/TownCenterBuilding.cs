using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownCenterBuilding : Building {

    /*** Metodes per defecte de Unity ***/


    protected override void Start()
    {
        base.Start();
        objectName = "Town Center";
        actions = new string[] { "CivilUnit" };
    }

    protected override void Update()
    {
        base.Update();
    }

    /*** Metodes publics ***/

    public override void PerformAction(string actionToPerform)
    {
        base.PerformAction(actionToPerform);
        CreateUnit(actionToPerform);
    }

    public override bool CanAttack()
    {
        if (UnderConstruction() || hitPoints == 0) return false;
        return true;
    }


    /*** Metodes interns accessibles per les subclasses ***/

    protected override void UseWeapon()
    {
        base.UseWeapon();
    }

}
