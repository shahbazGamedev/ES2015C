using UnityEngine;

public class Sumerian_WallEntrance : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Wall Entrance";
		cost = 100;
		hitPoints = maxHitPoints = 700;
        baseDefense = 5;
        getModels("Prefabs/Sumerian_WallEntrance", "Prefabs/Sumerian_WallEntrance_onConstruction", "Prefabs/Sumerian_WallEntrance_Semidemolished");
    }
}
