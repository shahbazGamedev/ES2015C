﻿using UnityEngine;

public class Yamato_cavalry_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Yamato Cavalry Advanced";
		//gameObject.tag = "mility";
		baseMoveSpeed = 10;
		cost = 250;
		hitPoints = maxHitPoints = 200;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_cavalry_advanced_AC") as RuntimeAnimatorController;
		chargeSounds ("Yamato_cavalry");
    }
}