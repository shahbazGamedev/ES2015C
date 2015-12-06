using UnityEngine;

public class Yamato_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
        objectName = "Yamato Civil";
		cost = 100;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
        anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_civil_AC") as RuntimeAnimatorController;
		chargeSounds ("Yamato_civil");
        buildableBuildings = new RTSObjectType[] { RTSObjectType.BuildingTownCenter, RTSObjectType.BuildingArmyBuilding, RTSObjectType.BuildingWallTower,
            RTSObjectType.BuildingWallEntrance, RTSObjectType.BuildingWall, RTSObjectType.BuildingCivilHouse, RTSObjectType.BuildingAcademy, RTSObjectType.BuildingUniversity };
    }
}