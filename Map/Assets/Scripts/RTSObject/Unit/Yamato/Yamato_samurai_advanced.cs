using UnityEngine;

public class Yamato_samurai_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
        objectName = "Yamato Samurai Advanced";
		baseMoveSpeed = 3;
		cost = 300;
		hitPoints = maxHitPoints = 200;
		baseAttackStrength = 30;
        baseDefense = 10;
        baseAttackSpeed = 2.0f;
        anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_samurai_advanced_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Yamato_samurai_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Yamato_samurai_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Yamato_samurai_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Yamato_samurai_Die") as AudioClip;
    }
}