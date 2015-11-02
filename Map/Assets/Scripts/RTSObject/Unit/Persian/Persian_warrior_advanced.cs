﻿using UnityEngine;

public class Persian_warrior_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Persian Warrior Advanced";
		baseMoveSpeed = 3;
		cost = 300;
		hitPoints = 200;
		maxHitPoints = 200;
		baseAttackStrength = 30;
        baseDefense = 10;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Persian_warrior_advanced_AC") as RuntimeAnimatorController;
    }
}