using UnityEngine;

public class Sumerian_Academy : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Academy";
		cost = 400;
		hitPoints = maxHitPoints = 800;
        baseDefense = 5;
    }
}
