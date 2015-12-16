using UnityEngine;

public class Sumerian_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Town Center";
        baseDefense = 5;
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitCivil, RTSObjectType.UnitCivilAxe,
            RTSObjectType.UnitCivilPick, RTSObjectType.UnitCivilRack };
        getModels("Prefabs/Sumerian_TownCenter", "Prefabs/Sumerian_TownCenter_onConstruction", "Prefabs/Sumerian_TownCenter_Semidemolished");
    }
}
