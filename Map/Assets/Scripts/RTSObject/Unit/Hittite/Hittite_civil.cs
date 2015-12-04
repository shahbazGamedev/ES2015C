using UnityEngine;

public class Hittite_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Civil";
		cost = 100;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Hittite_civil_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Hittite_civil_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Hittite_civil_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Hittite_civil_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Hittite_civil_Die") as AudioClip;
		farmingSound = Resources.Load ("Sounds/Hittite_civil_Farming") as AudioClip;
		miningSound = Resources.Load ("Sounds/Hittite_civil_Mining") as AudioClip;
		woodCuttingSound = Resources.Load ("Sounds/Hittite_civil_WoodCutting") as AudioClip;
		buildingSound = Resources.Load ("Sounds/Hittite_civil_Building") as AudioClip;
        buildableBuildings = new RTSObjectType[] { RTSObjectType.BuildingTownCenter, RTSObjectType.BuildingArmyBuilding, RTSObjectType.BuildingWallTower,
            RTSObjectType.BuildingWallEntrance, RTSObjectType.BuildingWall, RTSObjectType.BuildingCivilHouse, RTSObjectType.BuildingAcademy, RTSObjectType.BuildingUniversity };
    }
}