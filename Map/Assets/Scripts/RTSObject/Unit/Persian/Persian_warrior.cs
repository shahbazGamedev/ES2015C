using UnityEngine;

public class Persian_warrior : Unit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Persian Warrior";
		baseMoveSpeed = 4;
		cost = 200;
		hitPoints = 150;
		maxHitPoints = 150;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Persian_warrior_AC") as RuntimeAnimatorController;
    }
}