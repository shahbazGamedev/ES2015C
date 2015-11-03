using UnityEngine;
using System.Collections;

public class CitizenRDV : MonoBehaviour {

	CitizenAI citizenAI;
	string otherRol;
	bool assignedRol;

	// Use this for initialization
	void Start () {
		this.citizenAI.target = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /* CODI COMENTAT - NO COMPILA (MERGE 03/11/2015, COMENTAT PER JOAN BRUGUERA)
	void OnTriggerEnter (Collider other){
		this.otherRol = other.gameObject.tag;
		this.citizenAI.resources[this.citizenAI.resources.GetLength()-1] (other.transform); // añadimos el recurso a la BBDD
		citizenAI.target = other.transform;
		switch (this.citizenAI.subState) {
		
		case 11:break;
			if(otherRol == "arbol" && !assignedRol){
				this.citizenAI.gameObject.tag="woodCutter";
				assignedRol=true;
			}
		case 12:break;
		case 13:break;
		}

	}
    */
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
