using UnityEngine;

public class Yamato_Wall : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Yamato Wall";
		cost = 50;
		hitPoints = maxHitPoints = 500;
        baseDefense = 5;
        buildingTime = 1.0f;
		getModels("Prefabs/Yamato_Wall", "Prefabs/Yamato_Wall_onConstruction", "Prefabs/Yamato_Wall");
    }
}
