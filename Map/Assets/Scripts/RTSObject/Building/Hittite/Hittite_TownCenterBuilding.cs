using UnityEngine;

public class Hittite_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Town Center";
        baseDefense = 5;
		actions = new string[] { "Civil Unit", "Civil Unit Axe"};
		getModels("Prefabs/Hittite_TownCenter", "Prefabs/Hittite_TownCenter_onConstruction", "Prefabs/Hittites_TownCenter_Semidemolished");
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
		}
		base.CreateUnit (unitName);
	}
}
