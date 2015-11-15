using UnityEngine;

public class Yamato_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Yamato Army Building";
		actions = new string[] {"Samurai", "Samurai Advanced"};
        baseDefense = 5;
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
		switch (unitName) {
		case "Samurai":
			creationUnit = Resources.Load ("Prefabs/Yamato_samurai") as GameObject;
			break;
		case "Samurai Advanced":
			creationUnit = Resources.Load ("Prefabs/Yamato_samurai_advanced") as GameObject;
			break;
		}
		base.CreateUnit (unitName);
	}
}
