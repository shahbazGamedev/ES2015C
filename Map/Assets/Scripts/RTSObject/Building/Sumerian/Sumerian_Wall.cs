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
        buildingTime = 1.0f;
        getModels("Prefabs/Sumerian_Wall", "Prefabs/Sumerian_Wall_onConstruction", "Prefabs/Sumerian_Wall_Semidemolished");
    }
}
