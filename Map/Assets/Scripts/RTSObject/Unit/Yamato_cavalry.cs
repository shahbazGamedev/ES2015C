using UnityEngine;

public class Yamato_cavalry : Unit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Yamato Cavalry";
		baseMoveSpeed = 10;
		cost = 300;
		hitPoints = 150;
		maxHitPoints = 150;
		baseAttackStrength = 20;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
        anim.runtimeAnimatorController = Resources.Load("AnimatorControllers/Yamato_cavalry_AC") as RuntimeAnimatorController;
    }
}