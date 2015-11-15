using UnityEngine;

public class Sumerian_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
        objectName = "Sumerian Civil";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
		baseBuildSpeed = 50;
        anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Sumerian_civil_AC") as RuntimeAnimatorController;
		actions = new string[] { "Town Center" };
    }

    /*** Metodes interns accessibles per les subclasses ***/

	public override void PerformAction(string actionToPerform)
	{
		switch (actionToPerform) {
		case "Town Center":
			creationBuilding = Resources.Load ("Prefabs/Sumerian_TownCenter") as GameObject;
            creationBuildingConstruction = Resources.Load("Prefabs/Sumerian_TownCenter") as GameObject;
            break;
		}
	}
}