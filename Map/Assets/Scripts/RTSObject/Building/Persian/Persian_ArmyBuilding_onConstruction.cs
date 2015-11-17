using UnityEngine;

public class Persian_ArmyBuildingConstruction : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Persian Army Building Construction";
        baseDefense = 5;
		hitPoints=0;
    }
	
	
}
