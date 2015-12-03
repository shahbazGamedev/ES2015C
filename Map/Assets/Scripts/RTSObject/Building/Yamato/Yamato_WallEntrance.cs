using UnityEngine;

public class Yamato_WallEntrance : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Yamato Wall Entrance";
		cost = 100;
		hitPoints = maxHitPoints = 700;
        baseDefense = 5;
		getModels("Prefabs/Yamato_WallEntrance", "Prefabs/Yamato_WallEntrance_onConstruction", "Prefabs/Yamato_WallEntrance_Semidemolished");
    }
}
