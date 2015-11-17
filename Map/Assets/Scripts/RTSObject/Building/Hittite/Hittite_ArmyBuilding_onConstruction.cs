using UnityEngine;

public class Hittite_ArmyBuildingConstruction : ArmyBuilding
{

	/*** Metodes per defecte de Unity ***/

	protected override void Awake ()
	{
		base.Awake ();
		objectName = "Hittite Army Building Construction";
        baseDefense = 5;
		hitPoints=0;
    }
	
	
}
