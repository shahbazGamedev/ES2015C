using UnityEngine;

public class Hittite_WallTower : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Wall Tower";
		cost = 250;
		hitPoints = maxHitPoints = 900;
        baseDefense = 5;
		getModels("Prefabs/Hittite_WallTower", "Prefabs/Hittite_WallTower_onConstruction", "Prefabs/Hittite_WallTower_Semidemolished");
    }
}
