using UnityEngine;

public class Sumerian_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Sumerian Army Building";
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitArcher, RTSObjectType.UnitCavalry, RTSObjectType.UnitWarrior};
        baseDefense = 5;
		getModels("Prefabs/Sumerian_ArmyBuilding", "Prefabs/Sumerian_ArmyBuilding_onConstruction", "Prefabs/Sumerian_ArmyBuilding_Semidemolished");
    }
}
