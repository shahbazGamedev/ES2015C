using UnityEngine;

public class Hittite_warrior : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Warrior";
		baseMoveSpeed = 4;
		cost = 200;
		hitPoints = maxHitPoints = 150;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Hittite_warrior_AC") as RuntimeAnimatorController;
		walkingSound = Resources.Load ("Sounds/Hittite_warrior_Walk") as AudioClip;
		runningSound = Resources.Load ("Sounds/Hittite_warrior_Run") as AudioClip;
		fightSound = Resources.Load ("Sounds/Hittite_warrior_Fight") as AudioClip;
		dieSound = Resources.Load ("Sounds/Hittite_warrior_Die") as AudioClip;
    }
}