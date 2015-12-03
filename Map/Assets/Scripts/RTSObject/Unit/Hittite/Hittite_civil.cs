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
		actions = new string[] { "Town Center", "Army Building", "Wall Tower", "Wall Entrance", "Wall", "Civil House", "Academy", "University"};
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	public override void PerformAction(string actionToPerform)
	{
		switch (actionToPerform) {
            case "Town Center":
                StartBuildingLocationSelection("Prefabs/Hittite_TownCenter");
                break;
            case "Army Building":
                StartBuildingLocationSelection("Prefabs/Hittite_ArmyBuilding");
			    break;
		    case "Wall Tower":
                StartBuildingLocationSelection("Prefabs/Hittite_WallTower");
                break;
		    case "Wall Entrance":
                StartBuildingLocationSelection("Prefabs/Hittite_WallEntrance");
                break;
		    case "Wall":
                StartBuildingLocationSelection("Prefabs/Hittite_Wall");
                break;
		    case "Civil House":
                StartBuildingLocationSelection("Prefabs/Hittite_CivilHouse");
                break;
		    case "Academy":
                StartBuildingLocationSelection("Prefabs/Hittite_Academy");
                break;
            case "University":
                StartBuildingLocationSelection("Prefabs/Hittite_University");
                break;
		}
	}
}