using UnityEngine;

public class Wood : Resource {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		resourceType = ResourceType.Wood;
		objectName = "Wood Resource";
		gameObject.tag = "tree";
		hitPoints = 100;
		maxHitPoints = 100;
    }
}
