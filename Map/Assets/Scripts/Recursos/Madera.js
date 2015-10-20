#pragma strict

function Start () {

}
var cantidad : int = 150;
var herramienta : GameObject;
var acabado : boolean = false;

function Update () {
    if (cantidad <= 0){
        acabado = true;     
    }
    if(acabado == true){
        Destroy(this.gameObject,4);
    }
}

function OnTriggerEnter (herramienta : Collider){
    cantidad -= 3;
    Recolector.recolMadera += 3;
}