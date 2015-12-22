using UnityEngine;

public class Persian_archer : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Archer";
		//gameObject.tag = "mility";
		baseMoveSpeed = 6;
		cost = 175;
		hitPoints = maxHitPoints = 125;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackRange = 8;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Persian_archer_AC") as RuntimeAnimatorController;
		chargeSounds ("Persian_archer");
    }
}