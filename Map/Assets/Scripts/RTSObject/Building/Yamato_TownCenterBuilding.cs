using UnityEngine;

public class Yamato_TownCenterBuilding : TownCenterBuilding {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Yamato Town Center";
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
		switch (unitName) {
		case "Civil Unit":
			creationUnit = Resources.Load ("Prefabs/Yamato_civil") as GameObject;
			break;
		}
		base.CreateUnit (unitName);
	}
}
