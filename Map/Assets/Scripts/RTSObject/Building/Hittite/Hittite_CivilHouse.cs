﻿using UnityEngine;

public class Hittite_CivilHouse : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Civil House";
		cost = 50;
		hitPoints = maxHitPoints = 200;
        baseDefense = 5;
    }
}
