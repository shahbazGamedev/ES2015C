using UnityEngine;

public class Persian_Wall : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Wall";
		cost = 50;
		hitPoints = maxHitPoints = 500;
        baseDefense = 5;
        buildingTime = 1.0f;
        getModels("Prefabs/Persian_Wall", "Prefabs/Persian_Wall_onConstruction", "Prefabs/Persian_Wall_Semidemolished");
    }
}
