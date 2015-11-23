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
		actions = new string[] { "Town Center", "Army Building", "Wall Tower", "Wall Entrance", "Wall", "Civil House", "Academy"};
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	public override void PerformAction(string actionToPerform)
	{
		switch (actionToPerform) {
            case "Town Center":
                StartBuildingLocationSelection("Prefabs/Hittite_TownCenter", "Prefabs/Hittite_TownCenter");
                break;
            case "Army Building":
                StartBuildingLocationSelection("Prefabs/Hittite_ArmyBuilding", "Prefabs/Hittite_ArmyBuildingConstruction");
			    break;
		    case "Wall Tower":
                StartBuildingLocationSelection("Prefabs/Hittite_WallTower", "Prefabs/Hittite_WallTower");
                break;
		    case "Wall Entrance":
                StartBuildingLocationSelection("Prefabs/Hittite_WallEntrance", "Prefabs/Hittite_WallEntrance");
                break;
		    case "Wall":
                StartBuildingLocationSelection("Prefabs/Hittite_Wall", "Prefabs/Hittite_Wall");
                break;
		    case "Civil House":
                StartBuildingLocationSelection("Prefabs/Hittite_CivilHouse", "Prefabs/Hittite_CivilHouse");
                break;
		    case "Academy":
                StartBuildingLocationSelection("Prefabs/Hittite_Academy", "Prefabs/Hittite_Academy");
                break;
		}
	}
}