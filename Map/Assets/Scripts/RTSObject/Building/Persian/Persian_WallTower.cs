using UnityEngine;

public class Persian_WallTower : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Wall Tower";
		cost = 250;
		hitPoints = maxHitPoints = 900;
        baseDefense = 5;
    }
}
