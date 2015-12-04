using UnityEngine;

public class Persian_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Persian Army Building";
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitArcher, RTSObjectType.UnitArcherAdvanced, RTSObjectType.UnitCavalry,
            RTSObjectType.UnitCavalryAdvanced, RTSObjectType.UnitWarrior, RTSObjectType.UnitWarriorAdvanced };
        baseDefense = 5;
        getModels("Prefabs/Persian_ArmyBuilding", "Prefabs/Persian_ArmyBuilding_onConstruction", "Prefabs/Persian_ArmyBuilding_Semidemolished");
    }
}