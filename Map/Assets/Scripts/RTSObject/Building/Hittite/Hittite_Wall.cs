using UnityEngine;

public class Hittite_Wall : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Hittite Wall";
		cost = 50;
		hitPoints = 0;
		maxHitPoints = 500;
        baseDefense = 5;
    }
}
