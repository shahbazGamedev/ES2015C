using UnityEngine;

public class Hittite_Academy : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Hittite Academy";
		cost = 400;
		hitPoints = 0;
		maxHitPoints = 800;
        baseDefense = 5;
    }
}
