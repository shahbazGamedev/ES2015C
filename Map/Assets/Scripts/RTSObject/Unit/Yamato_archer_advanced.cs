using UnityEngine;

public class Yamato_archer_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Yamato Archer Advanced";
		moveSpeed = 4;
		cost = 200;
		hitPoints = 150;
		maxHitPoints = 150;
		hitDamage = 25;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_archer_advanced_AC") as RuntimeAnimatorController;
    }
}