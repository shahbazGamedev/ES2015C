using UnityEngine;

public class Hittite_Academy : Academy {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Academy";
		cost = 400;
		hitPoints = maxHitPoints = 800;
        baseDefense = 5;
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitArcherAdvanced, RTSObjectType.UnitCavalryAdvanced, RTSObjectType.UnitWarriorAdvanced };
		getModels("Prefabs/Hittite_Academy", "Prefabs/Hittite_Academy_onConstruction", "Prefabs/Hittite_Academy_Semidemolished");
    }
}
