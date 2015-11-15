using UnityEngine;

public class ArmyBuilding : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Army Building";
		cost = 500;
		hitPoints = 0;
		maxHitPoints = 3000;
        baseDefense = 5;
    }

    protected override void Update()
    {
        base.Update();
    }
}
