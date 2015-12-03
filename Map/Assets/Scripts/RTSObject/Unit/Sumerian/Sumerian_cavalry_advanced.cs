using UnityEngine;

public class Sumerian_cavalry_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Cavalry Advanced";
		//gameObject.tag = "mility";
		baseMoveSpeed = 10;
		cost = 250;
		hitPoints = maxHitPoints = 200;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Sumerian_cavalry_advanced_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Sumerian_cavalry_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Sumerian_cavalry_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Sumerian_cavalry_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Sumerian_cavalry_Die") as AudioClip;
    }
}