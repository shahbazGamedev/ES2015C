﻿using UnityEngine;

public class Yamato_CivilHouse : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		objectName = "Yamato Civil House";
		cost = 50;
		hitPoints = 0;
		maxHitPoints = 200;
        baseDefense = 5;
    }
}
