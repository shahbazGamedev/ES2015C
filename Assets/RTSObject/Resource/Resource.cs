using UnityEngine;
using System.Collections;

public class Resource : RTSObject {

    public enum ResourceType { Money, Power, Ore, Wood }
    public float capacity;

    protected float amountLeft;
    protected ResourceType resourceType;
    
    protected override void Start () {
	
	}
    
    protected override void Update () {
	
	}
}
