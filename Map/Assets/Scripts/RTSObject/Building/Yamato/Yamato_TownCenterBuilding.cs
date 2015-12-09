using UnityEngine;

public class Yamato_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
		base.Awake();
		objectName = "Yamato Town Center";
        baseDefense = 5;
		actions = new string[] { "Civil Unit", "Civil Unit Axe", "Civil Unit Pick", "Civil Unit Rack" };
		getModels("Prefabs/Yamato_TownCenter", "Prefabs/Yamato_TownCenter_onConstruction", "Prefabs/Yamato_TownCenter_Semidemolished");
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
		switch (unitName) {
		case "Civil Unit":
			creationUnit = Resources.Load ("Prefabs/Yamato_civil") as GameObject;
			break;
		case "Civil Unit Axe":
			creationUnit = Resources.Load ("Prefabs/Yamato_civil_axe") as GameObject;
			break;
		case "Civil Unit Pick":
			creationUnit = Resources.Load ("Prefabs/Yamato_civil_pick") as GameObject;
			break;
		case "Civil Unit Rack":
			creationUnit = Resources.Load ("Prefabs/Yamato_civil_rack") as GameObject;
			break;
		}
		base.CreateUnit (unitName);
	}
}
