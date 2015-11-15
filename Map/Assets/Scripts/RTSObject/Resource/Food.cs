using UnityEngine;

public class Food : Resource {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		resourceType = ResourceType.Food;
		objectName = "Food Resource";
		gameObject.tag = "food";
		amountLeft = capacity = hitPoints = maxHitPoints = 200;
    }
}
