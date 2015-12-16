using UnityEngine;

public class Sumerian_Academy : Academy {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Academy";
		cost = 400;
		hitPoints = maxHitPoints = 800;
        baseDefense = 5;
        spawnableUnits = new RTSObjectType[] { RTSObjectType.UnitArcherAdvanced, RTSObjectType.UnitCavalryAdvanced, RTSObjectType.UnitWarriorAdvanced };
        getModels("Prefabs/Sumerian_Academy", "Prefabs/Sumerian_Academy_onConstruction", "Prefabs/Sumerian_Academy_Semidemolished");
    }
}
