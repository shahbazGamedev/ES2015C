using UnityEngine;

public class Hittite_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Town Center";
        baseDefense = 5;
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitCivil, RTSObjectType.UnitCivilAxe,
            RTSObjectType.UnitCivilPick, RTSObjectType.UnitCivilRack };
        getModels("Prefabs/Hittite_TownCenter", "Prefabs/Hittite_TownCenter_onConstruction", "Prefabs/Hittite_TownCenter_Semidemolished");
    }
}
