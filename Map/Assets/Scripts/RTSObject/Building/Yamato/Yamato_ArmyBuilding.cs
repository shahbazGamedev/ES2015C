using UnityEngine;

public class Yamato_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Yamato Army Building";
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitArcher, RTSObjectType.UnitArcherAdvanced, RTSObjectType.UnitCavalry,
            RTSObjectType.UnitCavalryAdvanced, RTSObjectType.UnitWarrior, RTSObjectType.UnitWarriorAdvanced };
        baseDefense = 5;
		getModels("Prefabs/Yamato_ArmyBuilding", "Prefabs/Yamato_ArmyBuilding_onConstruction", "Prefabs/Yamato_ArmyBuilding_Semidemolished");
    }
}
