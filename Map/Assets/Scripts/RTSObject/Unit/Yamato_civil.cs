using UnityEngine;

public class Yamato_civil : CivilUnit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Yamato Civil";
		cost = 50;
		hitDamage = 10;

		townCenter = Resources.Load ("Prefabs/Yamato_TownCenter") as GameObject;
		armyBuilding = Resources.Load ("Prefabs/Yamato_ArmyBuilding") as GameObject;
		Debug.Log (townCenter);
		Debug.Log (armyBuilding);
    }
}