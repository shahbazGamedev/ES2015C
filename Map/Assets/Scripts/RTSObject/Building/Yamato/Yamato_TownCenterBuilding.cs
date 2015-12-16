using UnityEngine;

public class Yamato_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
		base.Awake();
		objectName = "Yamato Town Center";
        baseDefense = 5;
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitCivil, RTSObjectType.UnitCivilAxe,
            RTSObjectType.UnitCivilPick, RTSObjectType.UnitCivilRack };
        getModels("Prefabs/Yamato_TownCenter", "Prefabs/Yamato_TownCenter_onConstruction", "Prefabs/Yamato_TownCenter_Semidemolished");
    }
}
