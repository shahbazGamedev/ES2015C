using UnityEngine;

public class Yamato_samurai_advanced : Unit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Yamato Samurai Advanced";
		moveSpeed = 10;
		cost = 300;
		hitPoints = 200;
		maxHitPoints = 200;
		hitDamage = 30;
    }
}