using UnityEngine;
using System.Collections;

public class CitizenRDV : MonoBehaviour {

	public CitizenAI citizenAI;
	string otherRol;
	bool assignedRol;

	// Use this for initialization
	void Start () {
		assignedRol = false;
		//citizenAI = this.gameObject.GetComponent<CitizenAI>();
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    // CODI COMENTAT - NO COMPILA (MERGE 03/11/2015, COMENTAT PER JOAN BRUGUERA)
	void OnTriggerEnter (Collider other){
		otherRol = other.tag;
		citizenAI.setState(2);
		//this.citizenAI.resources[this.citizenAI.resources.GetLength()-1] (other.transform); // añadimos el recurso a la BBDD
		citizenAI.setTarget(other.gameObject.transform);
		if (otherRol == "tree" && !this.assignedRol) {
			citizenAI.tag="woodCutter";
			//citizenAI.target = other.transform;
			assignedRol = true;
			citizenAI.setState(2);
		}

	}

}

/*
function OnTriggerEnter(other:Collider){
	
	otherRol = other.gameObject.tag;
	recursos.Add(other.transform);	
	enemigo.objetivo=other.transform;
	
	if(otherRol == "arbol" && !rolAsignado){
		enemigo.gameObject.tag="lenyador";
		rolAsignado=true;
		enemigo.estado=2;
	}
}


function OnTriggerExit(other:Collider){
	rolAsignado = false;
	enemigo.estado=1;
	gameObject.GetComponent.<Collider>().isTrigger=false;
	gameObject.GetComponent.<Collider>().isTrigger=true;
	enemigo.gameObject.tag = "neutro";
}
*/
