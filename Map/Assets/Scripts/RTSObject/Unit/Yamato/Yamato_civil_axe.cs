﻿using UnityEngine;

public class Yamato_civil_axe : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
        objectName = "Yamato Civil Axe";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
        anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_civil_axe_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Yamato_civil_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Yamato_civil_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Yamato_civil_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Yamato_civil_Die") as AudioClip;
		farmingSound = Resources.Load ("Sounds/Yamato_civil_Farming") as AudioClip;
		miningSound = Resources.Load ("Sounds/Yamato_civil_Mining") as AudioClip;
		woodCuttingSound = Resources.Load ("Sounds/Yamato_civil_WoodCutting") as AudioClip;
		buildingSound = Resources.Load ("Sounds/Yamato_civil_Building") as AudioClip;
    }
}