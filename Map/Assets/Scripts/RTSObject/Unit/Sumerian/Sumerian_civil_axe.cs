using UnityEngine;

public class Sumerian_civil_axe : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Civil Axe";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
		baseBuildSpeed = 50;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Sumerian_civil_axe_AC") as RuntimeAnimatorController;
    }
}