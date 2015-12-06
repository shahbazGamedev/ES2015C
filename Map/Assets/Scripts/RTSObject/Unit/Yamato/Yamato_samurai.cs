using UnityEngine;

public class Yamato_samurai : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
        objectName = "Yamato Samurai";
		baseMoveSpeed = 4;
		cost = 200;
		hitPoints = maxHitPoints = 150;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
        anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_samurai_AC") as RuntimeAnimatorController;
		chargeSounds ("Yamato_samurai");
    }
}