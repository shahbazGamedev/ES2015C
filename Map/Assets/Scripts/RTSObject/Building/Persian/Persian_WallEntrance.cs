using UnityEngine;

public class Persian_WallEntrance : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Wall Entrance";
		cost = 100;
		hitPoints = maxHitPoints = 700;
        baseDefense = 5;
        getModels("Prefabs/Persian_WallEntrance", "Prefabs/Persian_WallEntrance_onConstruction", "Prefabs/Persian_WallEntrance_Semidemolished");
    }
}
