using UnityEngine;

public class Yamato_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
        objectName = "Yamato Civil";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
        anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_civil_AC") as RuntimeAnimatorController;
		actions = new string[] { "Town Center", "Army Building", "Wall Tower", "Wall Entrance", "Wall", "Civil House", "Academy" };
    }

    /*** Metodes interns accessibles per les subclasses ***/

	public override void PerformAction(string actionToPerform)
	{
		switch (actionToPerform) {
		    case "Town Center":
                StartBuildingLocationSelection("Prefabs/Yamato_TownCenter", "Prefabs/Yamato_TownCenter_onConstruction");
                break;
		    case "Army Building":
                StartBuildingLocationSelection("Prefabs/Yamato_ArmyBuilding", "Prefabs/Yamato_ArmyBuilding");
                break;
		    case "Wall Tower":
                StartBuildingLocationSelection("Prefabs/Yamato_WallTower", "Prefabs/Yamato_WallTower");
                break;
		    case "Wall Entrance":
                StartBuildingLocationSelection("Prefabs/Yamato_WallEntrance", "Prefabs/Yamato_WallEntrance");
                break;
		    case "Wall":
                StartBuildingLocationSelection("Prefabs/Yamato_Wall", "Prefabs/Yamato_Wall");
                break;
		    case "Civil House":
                StartBuildingLocationSelection("Prefabs/Yamato_CivilHouse", "Prefabs/Yamato_CivilHouse");
                break;
		    case "Academy":
                StartBuildingLocationSelection("Prefabs/Yamato_Academy", "Prefabs/Yamato_Academy");
                break;
		}
	}
}