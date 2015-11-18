﻿using UnityEngine;

public class Sumerian_cavalry : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Cavalry";
		baseMoveSpeed = 10;
		cost = 250;
		hitPoints = maxHitPoints = 200;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Sumerian_cavalry_AC") as RuntimeAnimatorController;
    }
}