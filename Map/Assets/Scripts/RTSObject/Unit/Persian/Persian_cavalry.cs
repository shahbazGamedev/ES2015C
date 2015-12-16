using UnityEngine;

public class Persian_cavalry: Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Cavalry";
		//gameObject.tag = "mility";
		baseMoveSpeed = 10;
		cost = 250;
		hitPoints = maxHitPoints = 200;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Persian_cavalry_AC") as RuntimeAnimatorController;
		chargeSounds ("Persian_cavalry");
    }
}