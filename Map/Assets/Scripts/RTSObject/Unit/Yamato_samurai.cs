using UnityEngine;

public class Yamato_samurai : Unit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Yamato Samurai";
		moveSpeed = 4;
		cost = 200;
		hitPoints = 150;
		maxHitPoints = 150;
		hitDamage = 25;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_samurai_AC") as RuntimeAnimatorController;
    }
}