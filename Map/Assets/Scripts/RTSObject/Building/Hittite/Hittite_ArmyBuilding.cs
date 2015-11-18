using UnityEngine;

public class Hittite_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Start ()
	{
		base.Start ();
		objectName = "Hittite Army Building";
		actions = new string[] {"Archer", "Cavalry", "Warrior", "Warrior Advanced"};
        baseDefense = 5;
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
		switch (unitName) {
		case "Archer":
			creationUnit = Resources.Load ("Prefabs/Hittite_archer") as GameObject;
			break;
		case "Cavalry":
			creationUnit = Resources.Load ("Prefabs/Hittite_cavalry") as GameObject;
			break;
		case "Warrior":
			creationUnit = Resources.Load ("Prefabs/Hittite_warrior") as GameObject;
			break;
		case "Warrior Advanced":
			creationUnit = Resources.Load ("Prefabs/Hittite_warrior_advanced") as GameObject;
			break;
		}
		base.CreateUnit (unitName);
	}
}
