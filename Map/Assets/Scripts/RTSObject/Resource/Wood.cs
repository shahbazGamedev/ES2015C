using UnityEngine;

public class Wood : Resource {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
        base.Awake();
		resourceType = ResourceType.Wood;
		objectName = "Wood Resource";
<<<<<<< HEAD
		gameObject.tag = "tree";
		hitPoints = 100;
		maxHitPoints = 100;
=======
		gameObject.tag = "wood";
		amountLeft = capacity = hitPoints = maxHitPoints = 100;
>>>>>>> origin/dev
    }
}
