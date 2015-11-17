using UnityEngine;

public class Hittite_Academy : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Academy";
		cost = 400;
		hitPoints = maxHitPoints = 800;
        baseDefense = 5;
    }
}
