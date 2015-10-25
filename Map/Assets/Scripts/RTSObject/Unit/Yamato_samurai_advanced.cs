using UnityEngine;

public class Yamato_samurai_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Yamato Samurai Advanced";
		moveSpeed = 3;
		cost = 300;
		hitPoints = 200;
		maxHitPoints = 200;
		hitDamage = 30;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_samurai_advanced_AC") as RuntimeAnimatorController;
    }
}