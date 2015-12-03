using UnityEngine;

public class Persian_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Persian Army Building";
		actions = new string[] {"Archer", "Archer Advanced", "Cavalry", "Cavalry Advanced", "Warrior","Warrior Advanced"};
        baseDefense = 5;
        getModels("Prefabs/Persian_ArmyBuilding", "Prefabs/Persian_ArmyBuilding_onConstruction", "Prefabs/Persian_ArmyBuilding_Semidemolished");
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
		switch (unitName) {
		case "Archer":
			creationUnit = Resources.Load ("Prefabs/Persian_archer") as GameObject;
			break;
		case "Archer Advanced":
			creationUnit = Resources.Load ("Prefabs/Persian_archer_advanced") as GameObject;
			break;
		case "Cavalry":
			creationUnit = Resources.Load ("Prefabs/Persian_cavalry") as GameObject;
			break;
		case "Cavalry Advanced":
			creationUnit = Resources.Load ("Prefabs/Persian_cavalry_advanced") as GameObject;
			break;
		case "Warrior":
			creationUnit = Resources.Load ("Prefabs/Persian_warrior") as GameObject;
			break;
		case "Warrior Advanced":
			creationUnit = Resources.Load ("Prefabs/Persian_warrior_advanced") as GameObject;
			break;
		}
		base.CreateUnit (unitName);
	}
}