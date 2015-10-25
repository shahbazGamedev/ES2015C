﻿using UnityEngine;

public class Yamato_ArmyBuilding : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Start ()
	{
		base.Start ();
		objectName = "Yamato Army Building";
		actions = new string[] {
			"Archer Advanced",
			"Cavalry",
			"Samurai",
			"Samurai Advanced"
		};
	}

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateUnit (string unitName)
	{
		switch (unitName) {
		case "Archer Advanced":
			creationUnit = Resources.Load ("Prefabs/Yamato_archer_advanced") as GameObject;
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
		base.CreateUnit (unitName);
	}
}
