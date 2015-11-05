using UnityEngine;

public class TownCenterBuilding : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Town Center";
		gameObject.tag = "townCenter";
		cost = 800;
		hitPoints = 0;
		maxHitPoints = 5000;
        baseAttackStrength = 10;
        baseAttackSpeed = 1.0f;
        baseAttackRange = 10;
        baseDefense = 10;
        actions = new string[] { "Civil Unit" };
    }

    protected override void Update()
    {
        base.Update();
    }
	
		protected override void Awake ()
	{
		base.Awake ();
	}


    /*** Metodes interns accessibles per les subclasses ***/

    protected override void UseWeapon()
    {
        base.UseWeapon();
    }

}
