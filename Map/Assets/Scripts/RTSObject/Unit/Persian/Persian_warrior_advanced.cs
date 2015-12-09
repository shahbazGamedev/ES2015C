using UnityEngine;

public class Persian_warrior_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Warrior Advanced";
		baseMoveSpeed = 3;
		cost = 300;
		hitPoints = maxHitPoints = 200;
		baseAttackStrength = 30;
        baseDefense = 10;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Persian_warrior_advanced_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Persian_warrior_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Persian_warrior_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Persian_warrior_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Persian_warrior_Die") as AudioClip;
    }
}