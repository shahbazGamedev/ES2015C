using UnityEngine;

public class Hittite_WallEntrance : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Hittite Wall Entrance";
		cost = 100;
		hitPoints = 0;
		maxHitPoints = 700;
        baseDefense = 5;
    }
}
