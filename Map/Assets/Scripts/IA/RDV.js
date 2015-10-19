#pragma strict
//Rango De Vision
var enemigo : IA;
var otherRol= "";
var rolAsignado : boolean;
var recursos = new Array ();

function Start () {
	enemigo.objetivo = null;
}

function Update () {

}

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
