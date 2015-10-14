#pragma strict

function Start () {

}

var espada : GameObject;
var vidaEnemigo : float = 100;
var muerto : boolean = false;

function Update () {
    if (muerto == false){
        vidaEnemigo += 0.5 * Time.deltaTime;
    }
    if(vidaEnemigo >= 100){
        vidaEnemigo = 100;
    }
    if (vidaEnemigo <= 0){
        muerto = true;     
    }
    if(muerto == true){
        //Vida_Player.exp += 10 * Time.deltaTime;
        Destroy(this.gameObject,4);
    }
}

function OnTriggerEnter (espada : Collider){
    vidaEnemigo -= 30;
}