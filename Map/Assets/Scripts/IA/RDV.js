#pragma strict
//Rango De Vision
var enemigo : IA;
var rol = "";
var otherRol= "";

function Start () {

}

function Update () {

}

function OnTriggerEnter(other:Collider){
	otherRol = other.gameObject.tag;
	rol = enemigo.gameObject.tag;
	
	if(otherRol=="arbol" && rol == "lenyador"){
		enemigo.estado=2;
		//enemigo.gameObject.GetComponent.<Collider>().isTrigger=false;
	}else if(otherRol=="mina" && rol == "minero"){
		enemigo.estado=2;
		//enemigo.gameObject.GetComponent.<Collider>().isTrigger=false;

	}else if(otherRol=="comida" && rol=="recolector"){
	
		//enemigo.gameObject.GetComponent.<Collider>().isTrigger=false;

		enemigo.estado=2;
	}
}
function OnTriggerExit(other:Collider){
	if(other.gameObject.tag=="arbol" && gameObject.tag == "lenyador"){
		enemigo.estado=1;
		gameObject.GetComponent.<Collider>().isTrigger=true;

	}
	if(other.gameObject.tag=="mina" && gameObject.tag == "minero"){
		enemigo.estado=1;
		gameObject.GetComponent.<Collider>().isTrigger=true;

	}
	if(other.gameObject.tag=="comida" && gameObject.tag=="recolector"){
		enemigo.estado=1;
		gameObject.GetComponent.<Collider>().isTrigger=true;

	}
}
