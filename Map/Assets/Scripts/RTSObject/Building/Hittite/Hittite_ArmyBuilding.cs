using UnityEngine;

public class Hittite_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Hittite Army Building";
		actions = new string[] {"Archer", "Archer Advanced", "Cavalry","Cavalry Advanced", "Warrior", "Warrior Advanced"};
        baseDefense = 5;
		getModels("Prefabs/Hittite_ArmyBuilding", "Prefabs/Hittite_ArmyBuilding_onConstruction", "Prefabs/Hittite_ArmyBuilding_Semidemolished");
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
        GameObject creationUnit = null;

		switch (unitName) {
		case "Archer":
			creationUnit = Resources.Load ("Prefabs/Hittite_archer") as GameObject;
			break;
		case "Archer Advanced":
			creationUnit = Resources.Load ("Prefabs/Hittite_archer_advanced") as GameObject;
			break;
		case "Cavalry":
			creationUnit = Resources.Load ("Prefabs/Hittite_cavalry") as GameObject;
			break;
		case "Cavalry Advanced":
			creationUnit = Resources.Load ("Prefabs/Hittite_cavalry_advanced") as GameObject;
			break;
		case "Warrior":
			creationUnit = Resources.Load ("Prefabs/Hittite_warrior") as GameObject;
			break;
		case "Warrior Advanced":
			creationUnit = Resources.Load ("Prefabs/Hittite_warrior_advanced") as GameObject;
			break;
		}

		AddUnitToCreationQueue(creationUnit);
	}
}
