using UnityEngine;

public class Persian_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Civil";
		cost = 100;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Persian_civil_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Persian_civil_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Persian_civil_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Persian_civil_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Persian_civil_Die") as AudioClip;
		farmingSound = Resources.Load ("Sounds/Persian_civil_Farming") as AudioClip;
		miningSound = Resources.Load ("Sounds/Persian_civil_Mining") as AudioClip;
		woodCuttingSound = Resources.Load ("Sounds/Persian_civil_WoodCutting") as AudioClip;
		buildingSound = Resources.Load ("Sounds/Persian_civil_Building") as AudioClip;
        buildableBuildings = new RTSObjectType[] { RTSObjectType.BuildingTownCenter, RTSObjectType.BuildingArmyBuilding, RTSObjectType.BuildingWallTower,
            RTSObjectType.BuildingWallEntrance, RTSObjectType.BuildingWall, RTSObjectType.BuildingCivilHouse, RTSObjectType.BuildingAcademy, RTSObjectType.BuildingUniversity };
    }
}