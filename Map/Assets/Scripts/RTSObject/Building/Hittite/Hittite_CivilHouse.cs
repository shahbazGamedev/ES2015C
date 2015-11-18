using UnityEngine;

public class Hittite_CivilHouse : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Hittite Civil House";
		cost = 50;
		hitPoints = 0;
		maxHitPoints = 200;
        baseDefense = 5;
    }
}
