using UnityEngine;

public class Hittite_archer : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Archer";
		baseMoveSpeed = 6;
		cost = 175;
		hitPoints = maxHitPoints = 125;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Hittite_archer_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Hittite_archer_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Hittite_archer_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Hittite_archer_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Hittite_archer_Die") as AudioClip;
    }
}