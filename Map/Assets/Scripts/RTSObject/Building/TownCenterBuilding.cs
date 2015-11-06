using UnityEngine;

public class TownCenterBuilding : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
        objectName = "Town Center";
		gameObject.tag = "townCenter";
		cost = 800;
		hitPoints = maxHitPoints = 5000;
        baseAttackStrength = 10;
        baseAttackSpeed = 1.0f;
        baseAttackRange = 10;
        baseDefense = 10;
    }

    protected override void Update()
    {
        base.Update();
    }


    /*** Metodes interns accessibles per les subclasses ***/

    protected override void UseWeapon()
    {
        base.UseWeapon();
    }

}
