using UnityEngine;

public class Sumerian_WallTower : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Wall Tower";
		cost = 250;
		hitPoints = maxHitPoints = 900;
        baseDefense = 5;
        getModels("Prefabs/Sumerian_WallTower", "Prefabs/Sumerian_WallTower_onConstruction", "Prefabs/Sumerian_WallTower_Semidemolished");
    }
}
