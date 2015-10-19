#pragma strict
// Rango De Ataque
var ataque : IA;
function Start () {

}

function Update () {

}

function OnTriggerEnter(other: Collider){
	if(other.gameObject.tag == "mina"||other.gameObject.tag=="arbol" || other.gameObject.tag=="comida"){
		ataque.estado = 3; // Pasamos a ataque, ejecutando la animacion oportuna
	}
}

function OnTriggerExit(other: Collider){
	ataque.estado = 1; // volvemos a patrullar
	
}