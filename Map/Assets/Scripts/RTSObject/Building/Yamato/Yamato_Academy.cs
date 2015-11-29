using UnityEngine;

public class Yamato_Academy : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Yamato Academy";
		cost = 400;
		hitPoints = maxHitPoints = 800;
        baseDefense = 5;
		getModels("Prefabs/Yamato_Academy", "Prefabs/Yamato_Academy", "Prefabs/Yamato Academy Smidemolished");
    }
}
