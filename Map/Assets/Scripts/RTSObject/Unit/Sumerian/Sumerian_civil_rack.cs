using UnityEngine;

public class Sumerian_civil_rack : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Sumerian Civil Rack";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
		anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Sumerian_civil_rack_AC") as RuntimeAnimatorController;
		chargeSounds ("Sumerian_civil");
    }
}