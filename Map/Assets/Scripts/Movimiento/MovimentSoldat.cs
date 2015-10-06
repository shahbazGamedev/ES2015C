using UnityEngine;
using System.Collections;

public class MovimentSoldat : MonoBehaviour {

    public Transform objectiu;
    NavMeshAgent soldat;

	// Use this for initialization
	void Start () {
        soldat = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        soldat.SetDestination (objectiu.position);
	}
}
