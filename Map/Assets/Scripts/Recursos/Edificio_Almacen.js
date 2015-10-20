#pragma strict

function Start () {

}
var vidaAlmacen : float = 1000;
static var almacenMadera : int = 0; //max = 15
static var almacenOro : int = 0; //max = 10
static var almacenComida : int = 0; //max = 20
var almacenMaderaVer : int = 0;
var almacenOroVer : int = 0;
var almacenComidaVer : int = 0;
var muerto : boolean = false;

function Update () {
    almacenMaderaVer = almacenMadera;
    almacenComidaVer = almacenComida;
    almacenOroVer = almacenOro;
}

function OnTriggerEnter (herramienta : Collider){
    almacenMadera += Recolector.recolMadera;
    almacenOro += Recolector.recolOro;
    almacenComida += Recolector.recolComida;
    Recolector.recolMadera = 0;
    Recolector.recolOro = 0;
    Recolector.recolComida = 0;
}