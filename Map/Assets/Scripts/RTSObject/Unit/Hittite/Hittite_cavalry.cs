using UnityEngine;

public class Hittite_cavalry : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Cavalry";
		//gameObject.tag = "mility";
		baseMoveSpeed = 10;
		cost = 250;
		hitPoints = maxHitPoints = 200;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Hittite_cavalry_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Hittite_cavalry_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Hittite_cavalry_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Hittite_cavalry_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Hittite_cavalry_Die") as AudioClip;
    }
}