using UnityEngine;

public class Hittite_warrior_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Warrior Advanced";
		//gameObject.tag = "mility";
		baseMoveSpeed = 3;
		cost = 300;
		hitPoints = maxHitPoints = 200;
		baseAttackStrength = 30;
        baseDefense = 10;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Hittite_warrior_advanced_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Hittite_warrior_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Hittite_warrior_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Hittite_warrior_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Hittite_warrior_Die") as AudioClip;
    }
}