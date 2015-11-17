using UnityEngine;

public class Persian_TownCenterBuildingConstruction : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Town Center Construction";
        baseDefense = 5;
        hitPoints = 0;
    }
}
