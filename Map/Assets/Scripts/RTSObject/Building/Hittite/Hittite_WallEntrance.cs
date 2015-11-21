﻿using UnityEngine;

public class Hittite_WallEntrance : Building {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		objectName = "Hittite Wall Entrance";
		cost = 100;
		hitPoints = maxHitPoints = 700;
        baseDefense = 5;
    }
}
