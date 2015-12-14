using UnityEngine;

public class Academy : Building {

    /*** Metodes per defecte de Unity ***/

    protected override void Awake()
    {
		base.Awake();
        objectName = "Academy";
        gameObject.tag = "academy";
    }

    protected override void Update()
    {
        base.Update();
    }
}