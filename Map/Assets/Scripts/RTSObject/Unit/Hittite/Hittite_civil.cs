using UnityEngine;

public class Hittite_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Civil";
		cost = 50;
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
		actions = new string[] { "Town Center", "Army Building", "Wall Tower", "Wall Entrance", "Wall", "Civil House", "Academy"};
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	public override void PerformAction(string actionToPerform)
	{
		switch (actionToPerform) {
            case "Town Center":
                StartBuildingLocationSelection("Prefabs/Hittite_TownCenter", "Prefabs/Hittite_TownCenter_onConstruction");
                break;
            case "Army Building":
                StartBuildingLocationSelection("Prefabs/Hittite_ArmyBuilding", "Prefabs/Hittite_ArmyBuilding_onConstruction");
			    break;
		    case "Wall Tower":
                StartBuildingLocationSelection("Prefabs/Hittite_WallTower", "Prefabs/Hittite_WallTower_onConstruction");
                break;
		    case "Wall Entrance":
                StartBuildingLocationSelection("Prefabs/Hittite_WallEntrance", "Prefabs/Hittite_WallEntrance_onConstruction");
                break;
		    case "Wall":
                StartBuildingLocationSelection("Prefabs/Hittite_Wall", "Prefabs/Hittite_Wall_onConstruction");
                break;
		    case "Civil House":
                StartBuildingLocationSelection("Prefabs/Hittite_CivilHouse", "Prefabs/Hittite_CivilHouse_onConstruction");
                break;
		    case "Academy":
                StartBuildingLocationSelection("Prefabs/Hittite_Academy", "Prefabs/Hittite_Academy_onConstruction");
                break;
		}
	}
}