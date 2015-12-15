using UnityEngine;

public class Yamato_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Yamato Army Building";
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitArcher, RTSObjectType.UnitCavalry, RTSObjectType.UnitWarrior};
		getModels("Prefabs/Yamato_ArmyBuilding", "Prefabs/Yamato_ArmyBuilding_onConstruction", "Prefabs/Yamato_ArmyBuilding_Semidemolished");
    }
}
