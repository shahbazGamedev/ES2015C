using UnityEngine;
using System.Collections;

public class Unit : RTSObject {



    public float moveSpeed = 5;

    protected bool moving;

    private Vector3 destination;
    private GameObject destinationTarget;
    
    protected override void Start () {
        actions = new string[] { "Move", "Attack" };
	}

    static int x = 0;
    protected override void Update () {
        if (x++ % 40 == 0)
        {
            TakeDamage(1);
            objectName = hitPoints.ToString();
        }
    }
}
