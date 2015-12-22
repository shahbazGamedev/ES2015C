using UnityEngine;

public class Hittite_archer_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Archer Advanced";
		//gameObject.tag = "mility";
		baseMoveSpeed = 6;
		cost = 175;
		hitPoints = maxHitPoints = 125;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackRange = 10;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Hittite_archer_advanced_AC") as RuntimeAnimatorController;
		chargeSounds ("Hittite_archer");
    }
}