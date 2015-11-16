using UnityEngine;

public class Persian_warrior : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Warrior";
		baseMoveSpeed = 4;
		cost = 200;
		hitPoints = maxHitPoints = 150;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Persian_warrior_AC") as RuntimeAnimatorController;
    }
}