using UnityEngine;

public class Persian_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Persian Army Building";
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitArcher, RTSObjectType.UnitCavalry, RTSObjectType.UnitWarrior};
        baseDefense = 5;
        getModels("Prefabs/Persian_ArmyBuilding", "Prefabs/Persian_ArmyBuilding_onConstruction", "Prefabs/Persian_ArmyBuilding_Semidemolished");
    }
}