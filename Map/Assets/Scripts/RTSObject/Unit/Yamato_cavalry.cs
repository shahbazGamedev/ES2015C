using UnityEngine;

public class Yamato_cavalry : Unit
{
    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Yamato Cavalry";
		moveSpeed = 10;
		cost = 300;
		hitPoints = 150;
		maxHitPoints = 150;
		hitDamage = 20;
    }
}