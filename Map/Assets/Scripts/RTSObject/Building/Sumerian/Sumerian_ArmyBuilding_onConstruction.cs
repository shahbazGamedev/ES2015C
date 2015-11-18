using UnityEngine;

public class Sumerian_ArmyBuildingConstruction : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Sumerian Army Building Construction";
        baseDefense = 5;
		hitPoints=0;
    }
	
	
}
