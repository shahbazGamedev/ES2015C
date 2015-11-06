using UnityEngine;

public class ArmyBuilding : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
		base.Awake();
        objectName = "Army Building";
		cost = 500;
		hitPoints = maxHitPoints = 3000;
        baseDefense = 5;
    }

    protected override void Update()
    {
        base.Update();
    }
}
