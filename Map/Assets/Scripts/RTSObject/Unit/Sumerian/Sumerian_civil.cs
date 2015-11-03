using UnityEngine;

public class Sumerian_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Sumerian Civil";
		cost = 50;
		baseAttackStrength = 10;
        baseDefense = 3;
        baseAttackSpeed = 1.0f;
		baseBuildSpeed=50;
        anim.runtimeAnimatorController = Resources.Load ("AnimatorControllers/Sumerian_civil_AC") as RuntimeAnimatorController;
		actions = new string[] { "Town Center" };
    }

	/*** Metodes interns accessibles per les subclasses ***/
	
	protected override void CreateBuilding(string buildingName)
	{
		switch (buildingName) {
		case "Town Center":
			creationBuilding = Resources.Load ("Prefabs/Sumerian_TownCenter") as GameObject;
			break;
		}
		base.CreateBuilding (buildingName);
	}
}