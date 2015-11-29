﻿using UnityEngine;

public class Yamato_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
        objectName = "Yamato Civil";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
        anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_civil_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Yamato_civil_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Yamato_civil_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Yamato_civil_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Yamato_civil_Die") as AudioClip;
		farmingSound = Resources.Load ("Sounds/Yamato_civil_Farming") as AudioClip;
		miningSound = Resources.Load ("Sounds/Yamato_civil_Mining") as AudioClip;
		woodCuttingSound = Resources.Load ("Sounds/Yamato_civil_WoodCutting") as AudioClip;
		buildingSound = Resources.Load ("Sounds/Yamato_civil_Building") as AudioClip;
		actions = new string[] { "Town Center", "Army Building", "Wall Tower", "Wall Entrance", "Wall", "Civil House", "Academy" };
    }

    /*** Metodes interns accessibles per les subclasses ***/

	public override void PerformAction(string actionToPerform)
	{
		switch (actionToPerform) {
		    case "Town Center":
                StartBuildingLocationSelection("Prefabs/Yamato_TownCenter", "Prefabs/Yamato_TownCenter_onConstruction");
                break;
		    case "Army Building":
                StartBuildingLocationSelection("Prefabs/Yamato_ArmyBuilding", "Prefabs/Yamato_ArmyBuilding_onConstruction");
                break;
		    case "Wall Tower":
                StartBuildingLocationSelection("Prefabs/Yamato_WallTower", "Prefabs/Yamato_WallTower_onConstruction");
                break;
		    case "Wall Entrance":
                StartBuildingLocationSelection("Prefabs/Yamato_WallEntrance", "Prefabs/Yamato_WallEntrance_onConstruction");
                break;
		    case "Wall":
                StartBuildingLocationSelection("Prefabs/Yamato_Wall", "Prefabs/Yamato_Wall_onConstruction");
                break;
		    case "Civil House":
                StartBuildingLocationSelection("Prefabs/Yamato_CivilHouse", "Prefabs/Yamato_CivilHouse_onConstruction");
                break;
		    case "Academy":
                StartBuildingLocationSelection("Prefabs/Yamato_Academy", "Prefabs/Yamato_Academy_onConstruction");
                break;
		}
	}
}