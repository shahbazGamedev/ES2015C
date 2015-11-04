using UnityEngine;

public class Yamato_Academy : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Yamato Academy";
		cost = 400;
		hitPoints = 0;
		maxHitPoints = 800;
        baseDefense = 5;
    }
}
