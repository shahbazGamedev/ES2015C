using UnityEngine;

public class Hittite_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Hittite Army Building";
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitArcher, RTSObjectType.UnitArcherAdvanced, RTSObjectType.UnitCavalry,
            RTSObjectType.UnitCavalryAdvanced, RTSObjectType.UnitWarrior, RTSObjectType.UnitWarriorAdvanced };
        baseDefense = 5;
		getModels("Prefabs/Hittite_ArmyBuilding", "Prefabs/Hittite_ArmyBuilding_onConstruction", "Prefabs/Hittite_ArmyBuilding_Semidemolished");
    }
}
