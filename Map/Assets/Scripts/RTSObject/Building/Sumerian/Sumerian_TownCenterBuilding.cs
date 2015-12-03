using UnityEngine;

public class Sumerian_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Town Center";
        baseDefense = 5;
		actions = new string[] { "Civil Unit", "Civil Unit Axe"};
		getModels("Prefabs/Sumerian_TownCenter", "Prefabs/Sumerian_TownCenter_onConstruction", "Prefabs/Sumerian_TownCenter_Semidemolished");
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
        GameObject creationUnit = null;

		switch (unitName) {
		case "Civil Unit":
			creationUnit = Resources.Load ("Prefabs/Sumerian_civil") as GameObject;
			break;
		case "Civil Unit Axe":
			creationUnit = Resources.Load ("Prefabs/Sumerian_civil_axe") as GameObject;
			break;
		}

        AddUnitToCreationQueue(creationUnit);
	}
}
