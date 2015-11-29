using UnityEngine;

public class Yamato_WallTower : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Yamato Wall Tower";
		cost = 250;
		hitPoints = maxHitPoints = 900;
        baseDefense = 5;
		getModels("Prefabs/Yamato_WallTower", "Prefabs/Yamato_WallTower", "Prefabs/Yamato_WallTower");
    }
}
