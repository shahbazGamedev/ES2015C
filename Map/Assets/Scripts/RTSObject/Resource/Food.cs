using UnityEngine;

public class Food : Resource {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		resourceType = ResourceType.Food;
		objectName = "Food Resource";
		gameObject.tag = "food";
		hitPoints = 200;
		maxHitPoints = 200;
    }
}
