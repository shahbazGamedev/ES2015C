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
		baseBuildSpeed = 50;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Hittite_civil_AC") as RuntimeAnimatorController;
		actions = new string[] { "Town Center", "Army Building", "Wall Tower", "Wall Entrance", "Wall", "Civil House", "Academy"};
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	public override void PerformAction(string actionToPerform)
	{
		switch (actionToPerform) {
		case "Town Center":
			creationBuilding = Resources.Load ("Prefabs/Hittite_TownCenter") as GameObject;
			break;
		case "Army Building":
			creationBuilding = Resources.Load ("Prefabs/Hittite_ArmyBuilding") as GameObject;
			creationBuildingConstruction = Resources.Load ("Prefabs/Hittite_ArmyBuildingConstruction") as GameObject;
			break;
		case "Wall Tower":
			creationBuilding = Resources.Load ("Prefabs/Hittite_WallTower") as GameObject;
			break;
		case "Wall Entrance":
			creationBuilding = Resources.Load ("Prefabs/Hittite_WallEntrance") as GameObject;
			break;
		case "Wall":
			creationBuilding = Resources.Load ("Prefabs/Hittite_Wall") as GameObject;
			break;
		case "Civil House":
			creationBuilding = Resources.Load ("Prefabs/Hittite_CivilHouse") as GameObject;
			break;
		case "Academy":
			creationBuilding = Resources.Load ("Prefabs/Hittite_Academy") as GameObject;
			break;
		}
	}
}