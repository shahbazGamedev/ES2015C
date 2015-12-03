using UnityEngine;

public class Yamato_archer_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Yamato Archer Advanced";
		//gameObject.tag = "mility";
		baseMoveSpeed = 6;
		cost = 175;
		hitPoints = maxHitPoints = 125;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_archer_advanced_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Yamato_archer_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Yamato_archer_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Yamato_archer_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Yamato_archer_Die") as AudioClip;
    }
}