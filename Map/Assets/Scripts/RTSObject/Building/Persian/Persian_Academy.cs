using UnityEngine;

public class Persian_Academy : Academy {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Academy";
		cost = 400;
		hitPoints = maxHitPoints = 800;
        baseDefense = 5;
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitArcherAdvanced, RTSObjectType.UnitCavalryAdvanced, RTSObjectType.UnitWarriorAdvanced };
        getModels("Prefabs/Persian_Academy", "Prefabs/Persian_Academy_onConstruction", "Prefabs/Persian_Academy_Semidemolished");
    }
}
