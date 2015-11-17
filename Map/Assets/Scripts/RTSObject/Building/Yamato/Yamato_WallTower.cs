using UnityEngine;

public class Yamato_WallTower : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Yamato Wall Tower";
		cost = 250;
		hitPoints = 0;
		maxHitPoints = 900;
        baseDefense = 5;
    }
}
