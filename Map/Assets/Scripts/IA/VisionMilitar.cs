using UnityEngine;
using System.Collections;

public class VisionMilitar : ADS {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.GetType(Player)) {
        target = other.gameObject.GetComponent<RTSObject>();
        currentDestination = other.gameObject.transform.position;
        colision = true;
        //}
    }
}
