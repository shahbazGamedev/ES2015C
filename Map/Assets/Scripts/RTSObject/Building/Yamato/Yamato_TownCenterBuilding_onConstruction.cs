using UnityEngine;

public class Yamato_TownCenterBuildingConstruction : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
		base.Awake();
		objectName = "Yamato Town Center Construction";
        baseDefense = 5;
        hitPoints = 0;
    }
}
