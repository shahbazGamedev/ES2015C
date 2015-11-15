using UnityEngine;

public class Persian_civil_axe : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Persian Civil Axe";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
		baseBuildSpeed=50;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Persian_civil_axe_AC") as RuntimeAnimatorController;
    }
}