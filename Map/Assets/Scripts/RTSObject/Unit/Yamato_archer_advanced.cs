using UnityEngine;

public class Yamato_archer_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Yamato Archer Advanced";
		baseMoveSpeed = 4;
		cost = 200;
		hitPoints = 150;
		maxHitPoints = 150;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
        baseAttackRange = 10;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Yamato_archer_advanced_AC") as RuntimeAnimatorController;
    }
}