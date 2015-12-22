using UnityEngine;

public class Yamato_CivilHouse : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Yamato Civil House";
		tag = "civilHouse";
		cost = 50;
		hitPoints = maxHitPoints = 200;
        baseDefense = 5;
		getModels("Prefabs/Yamato_CivilHouse", "Prefabs/Yamato_CivilHouse_onConstruction", "Prefabs/Yamato_civilHouse_SemiDemolished");
    }
}
