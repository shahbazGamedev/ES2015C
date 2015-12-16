using UnityEngine;

public class Persian_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Town Center";
        baseDefense = 5;
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitCivil, RTSObjectType.UnitCivilAxe,
            RTSObjectType.UnitCivilPick, RTSObjectType.UnitCivilRack };
        getModels("Prefabs/Persian_TownCenter", "Prefabs/Persian_TownCenter_onConstruction", "Prefabs/Persian_TownCenter_Semidemolished");
    }
}
