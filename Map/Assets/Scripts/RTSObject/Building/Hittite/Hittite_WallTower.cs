using UnityEngine;

public class Hittite_WallTower : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Hittite Wall Tower";
		cost = 250;
		hitPoints = 0;
		maxHitPoints = 900;
        baseDefense = 5;
    }
}
