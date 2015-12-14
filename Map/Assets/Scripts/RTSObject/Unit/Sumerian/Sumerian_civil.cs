using UnityEngine;

public class Sumerian_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
        objectName = "Sumerian Civil";
		cost = 100;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
        anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Sumerian_civil_AC") as RuntimeAnimatorController;
		chargeSounds ("Sumerian_civil");
        buildableBuildings = new RTSObjectType[] { RTSObjectType.BuildingTownCenter, RTSObjectType.BuildingArmyBuilding, RTSObjectType.BuildingWallTower,
            RTSObjectType.BuildingWallEntrance, RTSObjectType.BuildingWall, RTSObjectType.BuildingCivilHouse, RTSObjectType.BuildingAcademy };
    }
}