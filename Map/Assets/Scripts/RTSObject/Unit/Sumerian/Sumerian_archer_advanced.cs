using UnityEngine;

public class Sumerian_archer_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Archer Advanced";
		//gameObject.tag = "mility";
		baseMoveSpeed = 6;
		cost = 175;
		hitPoints = maxHitPoints = 125;
		baseAttackStrength = 25;
        baseDefense = 5;
        baseAttackSpeed = 2.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Sumerian_archer_advanced_AC") as RuntimeAnimatorController;
		chargeSounds ("Sumerian_archer");
    }
}