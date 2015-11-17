using UnityEngine;

public class Gold : Resource {

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
		resourceType = ResourceType.Gold;
		objectName = "Gold Resource";
		gameObject.tag = "gold";
		hitPoints = 800;
		maxHitPoints = 800;
    }
}
