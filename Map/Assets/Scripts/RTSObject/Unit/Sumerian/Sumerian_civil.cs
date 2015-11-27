using UnityEngine;

public class Sumerian_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
        objectName = "Sumerian Civil";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
        anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Sumerian_civil_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Sumerian_civil_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Sumerian_civil_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Sumerian_civil_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Sumerian_civil_Die") as AudioClip;
		farmingSound = Resources.Load ("Sounds/Sumerian_civil_Farming") as AudioClip;
		miningSound = Resources.Load ("Sounds/Sumerian_civil_Mining") as AudioClip;
		woodCuttingSound = Resources.Load ("Sounds/Sumerian_civil_WoodCutting") as AudioClip;
		buildingSound = Resources.Load ("Sounds/Sumerian_civil_Building") as AudioClip;
		actions = new string[] { "Town Center" };
    }

    /*** Metodes interns accessibles per les subclasses ***/

	public override void PerformAction(string actionToPerform)
	{
		switch (actionToPerform) {
		    case "Town Center":
                StartBuildingLocationSelection("Prefabs/Sumerian_TownCenter", "Prefabs/Sumerian_TownCenter_onConstruction");
                break;
		}
	}
}