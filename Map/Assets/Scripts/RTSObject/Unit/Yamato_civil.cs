using UnityEngine;

public class Yamato_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Yamato Civil";
		cost = 50;
		hitDamage = 10;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_civil_AC") as RuntimeAnimatorController;
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateBuilding(string buildingName)
	{
		switch (buildingName) {
		case "Town Center":
			creationBuilding = Resources.Load ("Prefabs/Yamato_TownCenter") as GameObject;
			break;
		case "Army Building":
			creationBuilding = Resources.Load ("Prefabs/Yamato_ArmyBuilding") as GameObject;
			break;
		case "Wall Tower":
			creationBuilding = Resources.Load ("Prefabs/Yamato_WallTower") as GameObject;
			break;
		case "Wall Entrance":
			creationBuilding = Resources.Load ("Prefabs/Yamato_WallEntrance") as GameObject;
			break;
		case "Wall":
			creationBuilding = Resources.Load ("Prefabs/Yamato_Wall") as GameObject;
			break;
		case "Civil House":
			creationBuilding = Resources.Load ("Prefabs/Yamato_CivilHouse") as GameObject;
			break;
		}
		base.CreateBuilding (buildingName);
	}
}