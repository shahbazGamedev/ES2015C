using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : RTSObject {

    public float maxBuildProgress = 10.0f;
    public float finishedJobVolume = 1.0f;

    protected Vector3 spawnPoint;
    protected Queue<string> buildQueue;

    private float currentBuildProgress = 0.0f;
    private bool needsBuilding = false;

    protected override void Start () {
	
	}
    
    protected override void Update () {
	
	}
}
