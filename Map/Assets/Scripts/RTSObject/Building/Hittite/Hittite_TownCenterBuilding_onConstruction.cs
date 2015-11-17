using UnityEngine;

public class Hittite_TownCenterBuildingConstruction : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Town Center Construction";
        baseDefense = 5;
        hitPoints = 0;
    }
}
