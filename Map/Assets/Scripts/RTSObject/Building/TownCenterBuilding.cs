using UnityEngine;

public class TownCenterBuilding : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Town Center";
		cost = 800;
		hitPoints = 5000;
		maxHitPoints = 5000;
        actions = new string[] { "Civil Unit" };
    }

    protected override void Update()
    {
        base.Update();
    }

    /*** Metodes publics ***/

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
