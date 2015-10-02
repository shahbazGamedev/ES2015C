using UnityEngine;
using System.Collections;

public class Unit : RTSObject {



    public float moveSpeed = 5;

    protected bool moving;

    private Vector3 destination;
    private GameObject destinationTarget;
    
    protected override void Start () {
	
	}
    
    protected override void Update () {
        Debug.Log("takedamage " + hitPoints.ToString());
        TakeDamage(1);
        objectName = hitPoints.ToString();
    }
}
