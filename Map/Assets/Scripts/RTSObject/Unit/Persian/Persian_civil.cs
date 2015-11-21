using UnityEngine;

public class Persian_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Persian Civil";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
		baseBuildSpeed = 50;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Persian_civil_AC") as RuntimeAnimatorController;
    }
}