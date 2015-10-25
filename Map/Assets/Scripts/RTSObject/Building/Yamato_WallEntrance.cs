using UnityEngine;

public class Yamato_WallEntrance : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Yamato Wall Entrance";
		cost = 100;
		hitPoints = 700;
		maxHitPoints = 700;
    }
}
