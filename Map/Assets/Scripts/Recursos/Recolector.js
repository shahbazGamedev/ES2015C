#pragma strict

function Start () {

}

var vidaRecolector : float = 100;
static var recolMadera : int = 0; //max = 15
static var recolOro : int = 0; //max = 10
static var recolComida : int = 0; //max = 20
var recolMaderaVer : int = 0;
var recolOroVer : int = 0;
var recolComidaVer : int = 0;
var muerto : boolean = false;

function Update () {
    recolMaderaVer = recolMadera;
    recolComidaVer = recolComida;
    recolOroVer = recolOro;

    if (muerto == false){
        vidaRecolector += 1 * Time.deltaTime;
    }
    if(vidaRecolector >= 100){
        vidaRecolector = 100;
    }
    if (vidaRecolector <= 0){
        muerto = true;     
    }
    if(muerto == true){
        Destroy(this.gameObject,4);
    }
    if(recolComida >= 20){
        //hacer un viaje
        //recolComida = 0;
    }
    if(recolOro >= 10){
        //hacer un viaje
        //recolOro = 0;
    }
    if(recolMadera >= 15){
        //hacer un viaje
        //recolMadera = 0;
    }
}
