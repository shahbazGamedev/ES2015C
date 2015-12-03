using UnityEngine;

public class Hittite_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Town Center";
        baseDefense = 5;
		actions = new string[] { "Civil Unit", "Civil Unit Axe", "Civil Unit Pick", "Civil Unit Rack"};
		getModels("Prefabs/Hittite_TownCenter", "Prefabs/Hittite_TownCenter_onConstruction", "Prefabs/Hittite_TownCenter_Semidemolished");
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
		switch (unitName) {
		case "Civil Unit":
			creationUnit = Resources.Load ("Prefabs/Hittite_civil") as GameObject;
			break;
		case "Civil Unit Axe":
			creationUnit = Resources.Load ("Prefabs/Hittite_civil_axe") as GameObject;
			break;
		case "Civil Unit Pick":
			creationUnit = Resources.Load ("Prefabs/Hittite_civil_pick") as GameObject;
			break;
		case "Civil Unit Rack":
			creationUnit = Resources.Load ("Prefabs/Hittite_civil_rack") as GameObject;
			break;
		}
		base.CreateUnit (unitName);
	}
}
