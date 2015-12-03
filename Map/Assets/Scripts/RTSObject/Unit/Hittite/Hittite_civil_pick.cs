using UnityEngine;

public class Hittite_civil_pick : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Civil Pick";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Hittite_civil_pick_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Hittite_civil_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Hittite_civil_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Hittite_civil_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Hittite_civil_Die") as AudioClip;
		farmingSound = Resources.Load ("Sounds/Hittite_civil_Farming") as AudioClip;
		miningSound = Resources.Load ("Sounds/Hittite_civil_Mining") as AudioClip;
		woodCuttingSound = Resources.Load ("Sounds/Hittite_civil_WoodCutting") as AudioClip;
		buildingSound = Resources.Load ("Sounds/Hittite_civil_Building") as AudioClip;
    }
}