using UnityEngine;

public class Gold : Resource {

    /*** Metodes per defecte de Unity ***/

	protected override void Awake()
    {
		base.Awake();
		resourceType = ResourceType.Gold;
		objectName = "Gold Resource";
		gameObject.tag = "gold";
		amountLeft = capacity = hitPoints = maxHitPoints = 800;
    }
}
