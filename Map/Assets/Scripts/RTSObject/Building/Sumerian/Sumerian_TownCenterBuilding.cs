using UnityEngine;

public class Sumerian_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Sumerian Town Center";
        baseDefense = 5;
		actions = new string[] { "Civil Unit", "Civil Unit Axe"};
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
		switch (unitName) {
		case "Civil Unit":
			creationUnit = Resources.Load ("Prefabs/Sumerian_civil") as GameObject;
			break;
		case "Civil Unit Axe":
			creationUnit = Resources.Load ("Prefabs/Sumerian_civil_axe") as GameObject;
			break;
		}
		base.CreateUnit (unitName);
	}
}
