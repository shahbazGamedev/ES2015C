using UnityEngine;

public class Persian_CivilHouse : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Civil House";
		tag = "civilHouse";
		cost = 50;
		hitPoints = maxHitPoints = 200;
        baseDefense = 5;
    }
}
