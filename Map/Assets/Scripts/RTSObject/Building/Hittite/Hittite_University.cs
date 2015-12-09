using UnityEngine;

public class Hittite_University : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite University";
		cost = 400;
		hitPoints = maxHitPoints = 800;
        baseDefense = 5;
        getModels("Prefabs/Hittite_University", "Prefabs/Hittite_University_onConstruction", "Prefabs/Hittite_University_Semidemolished");
    }
}
