using UnityEngine;

public class Sumerian_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Sumerian Army Building";
		actions = new string[] {"Archer", "Archer Advanced", "Cavalry", "Cavalry Advanced", "Warrior", "Warrior Advanced"};
        baseDefense = 5;
		getModels("Prefabs/Sumerian_ArmyBuilding", "Prefabs/Sumerian_ArmyBuilding_onConstruction", "Prefabs/Sumerian_ArmyBuilding_Semidemolished");
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
        GameObject creationUnit = null;

        switch (unitName) {
		case "Archer":
			creationUnit = Resources.Load ("Prefabs/Sumerian_archer") as GameObject;
			break;
		case "Archer Advanced":
			creationUnit = Resources.Load ("Prefabs/Sumerian_archer_advanced") as GameObject;
			break;
		case "Cavalry":
			creationUnit = Resources.Load ("Prefabs/Sumerian_cavalry") as GameObject;
			break;
		case "Cavalry Advanced":
			creationUnit = Resources.Load ("Prefabs/Sumerian_cavalry_advanced") as GameObject;
			break;
		case "Warrior":
			creationUnit = Resources.Load ("Prefabs/Sumerian_warrior") as GameObject;
			break;
		case "Warrior Advanced":
			creationUnit = Resources.Load ("Prefabs/Sumerian_warrior_advanced") as GameObject;
			break;
		}

        AddUnitToCreationQueue(creationUnit);
	}
}
