using UnityEngine;

public class Persian_Wall : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Wall";
		cost = 50;
		hitPoints = maxHitPoints = 500;
        baseDefense = 5;
        buildingTime = 1.0f;
    }
}
