using UnityEngine;

public class Yamato_samurai : Unit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Yamato Samurai";
		moveSpeed = 15;
		cost = 200;
		hitPoints = 150;
		maxHitPoints = 150;
		hitDamage = 25;
    }
}