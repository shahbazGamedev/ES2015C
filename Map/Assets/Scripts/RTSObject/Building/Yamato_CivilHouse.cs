﻿using UnityEngine;

public class Yamato_CivilHouse : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Yamato Civil House";
		cost = 50;
		hitPoints = 200;
		maxHitPoints = 200;
    }
}
