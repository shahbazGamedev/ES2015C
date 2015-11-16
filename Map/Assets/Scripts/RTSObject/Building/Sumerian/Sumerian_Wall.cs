using UnityEngine;

public class Sumerian_Wall : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Wall";
		cost = 50;
		hitPoints = maxHitPoints = 500;
        baseDefense = 5;
    }
}
