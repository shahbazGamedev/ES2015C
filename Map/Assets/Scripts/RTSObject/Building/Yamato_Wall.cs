using UnityEngine;

public class Yamato_Wall : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Yamato Wall";
		cost = 50;
		hitPoints = 500;
		maxHitPoints = 500;
        baseDefense = 5;
    }
}
