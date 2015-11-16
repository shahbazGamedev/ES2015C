using UnityEngine;

public class Sumerian_TownCenterBuilding_onConstruction : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Town Center";
        baseDefense = 5;
        hitPoints = 0;
    }
}
