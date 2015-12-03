using UnityEngine;

public class Yamato_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Yamato Army Building";
		actions = new string[] {"Archer", "Cavalry", "Samurai", "Samurai Advanced"};
        baseDefense = 5;
		getModels("Prefabs/Yamato_ArmyBuilding", "Prefabs/Yamato_ArmyBuilding_onConstruction", "Prefabs/Yamato Army Semidemolished");
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
        GameObject creationUnit = null;

		switch (unitName) {
		case "Archer":
			creationUnit = Resources.Load ("Prefabs/Yamato_archer") as GameObject;
			break;
		case "Cavalry":
			creationUnit = Resources.Load ("Prefabs/Yamato_cavalry") as GameObject;
			break;
		case "Samurai":
			creationUnit = Resources.Load ("Prefabs/Yamato_samurai") as GameObject;
			break;
		case "Samurai Advanced":
			creationUnit = Resources.Load ("Prefabs/Yamato_samurai_advanced") as GameObject;
			break;
		}

        AddUnitToCreationQueue(creationUnit);
    }
}
