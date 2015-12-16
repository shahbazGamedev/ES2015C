using UnityEngine;

public class Sumerian_CivilHouse : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Civil House";
		tag = "civilHouse";
		cost = 50;
		hitPoints = maxHitPoints = 200;
        baseDefense = 5;
    }
}
