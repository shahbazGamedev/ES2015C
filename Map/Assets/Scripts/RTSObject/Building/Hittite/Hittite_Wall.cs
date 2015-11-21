using UnityEngine;

public class Hittite_Wall : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Wall";
		cost = 50;
		hitPoints = maxHitPoints = 500;
        baseDefense = 5;
    }
}
