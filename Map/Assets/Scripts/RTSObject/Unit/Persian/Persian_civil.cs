﻿using UnityEngine;

public class Persian_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Civil";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Persian_civil_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Persian_civil_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Persian_civil_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Persian_civil_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Persian_civil_Die") as AudioClip;
		farmingSound = Resources.Load ("Sounds/Persian_civil_Farming") as AudioClip;
		miningSound = Resources.Load ("Sounds/Persian_civil_Mining") as AudioClip;
		woodCuttingSound = Resources.Load ("Sounds/Persian_civil_WoodCutting") as AudioClip;
		buildingSound = Resources.Load ("Sounds/Persian_civil_Building") as AudioClip;
		actions = new string[] { "Town Center", "Army Building", "Wall Tower", "Wall Entrance", "Wall", "Civil House", "Academy" };
    }

    /*** Metodes interns accessibles per les subclasses ***/

	public override void PerformAction(string actionToPerform)
	{
		switch (actionToPerform) {
		    case "Town Center":
                StartBuildingLocationSelection("Prefabs/Persian_TownCenter", "Prefabs/Persian_TownCenter_onConstruction");
                break;
		    case "Army Building":
                StartBuildingLocationSelection("Prefabs/Persian_ArmyBuilding", "Prefabs/Persian_ArmyBuilding_onConstruction");
                break;
		    case "Wall Tower":
                StartBuildingLocationSelection("Prefabs/Persian_WallTower", "Prefabs/Persian_WallTower_onConstruction");
                break;
		    case "Wall Entrance":
                StartBuildingLocationSelection("Prefabs/Persian_WallEntrance", "Prefabs/Persian_WallEntrance_onConstruction");
                break;
		    case "Wall":
                StartBuildingLocationSelection("Prefabs/Persian_Wall", "Prefabs/Persian_Wall_onConstruction");
                break;
		    case "Civil House":
                StartBuildingLocationSelection("Prefabs/Persian_CivilHouse", "Prefabs/Persian_CivilHouse_onConstruction");
                break;
		    case "Academy":
                StartBuildingLocationSelection("Prefabs/Persian_Academy", "Prefabs/Persian_Academy_onConstruction");
                break;
		}
	}
}