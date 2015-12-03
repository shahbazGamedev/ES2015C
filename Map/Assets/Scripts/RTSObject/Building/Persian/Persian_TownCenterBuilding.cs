using UnityEngine;

public class Persian_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Town Center";
        baseDefense = 5;
		actions = new string[] { "Civil Unit", "Civil Unit Axe" };
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
        GameObject creationUnit = null;

        switch (unitName) {
		case "Civil Unit":
			creationUnit = Resources.Load ("Prefabs/Persian_civil") as GameObject;
			break;
		case "Civil Unit Axe":
			creationUnit = Resources.Load ("Prefabs/Persian_civil_axe") as GameObject;
			break;
		}

        AddUnitToCreationQueue(creationUnit);
    }
}
