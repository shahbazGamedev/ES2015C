using UnityEngine;

public class Persian_Academy : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Academy";
		cost = 400;
		hitPoints = maxHitPoints = 800;
        baseDefense = 5;
        getModels("Prefabs/Persian_Academy", "Prefabs/Persian_Academy_onConstruction", "Prefabs/Persian_Academy_Semidemolished");
    }
}
