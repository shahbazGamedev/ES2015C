#pragma strict
// estado
var estado : int = 0;
//animaciones
var nada : AnimationClip;
var caminar : AnimationClip;
var correr : AnimationClip;
var morir : AnimationClip;
var atacar : AnimationClip;
var otherTag = "";

var pat : float = 0;
var patrullando : boolean = true;
var direccion : float= 1;

//
var player : Transform;
var Mina : Transform;
var Arbol : Transform;
var Comida : Transform;

var vel_seguir : float = 3;

function Start () {
	if(this.tag=="recolector"){
		player=Comida;
	}else if (this.tag == "minero"){
		player=Mina;
	}else if(this.tag=="lenyador"){
		player = Arbol;
	}
}

function Update () {
	transform.rotation.x=0;
	transform.rotation.z=0;

	if(pat <= 0 ){
		patrullando = false;		
		estado = 0;
		direccion *= -1;
	}		
	if(pat >= 10){
		patrullando = true;
		estado = 1;
	}
	if (patrullando == true && estado == 1){
		pat-=1 * Time.deltaTime;
	}
	if (patrullando == false && estado == 0){
		pat+=1 * Time.deltaTime;
	}
	
	
	
	if(estado == 0){ // no hacer nada
		Nada();
	}
	if(estado == 1){ //patrullar
		Patrullar();
	} 
	if(estado == 2){ //perseguir
		Perseguir();
	} 
	if(estado == 3){ //atacar
		Atacar();
	} 
	if(estado == 4){ //morir
		Morir();
	} //morir
	if(estado == 5){// esquivar
		Esquivar();
	}
	
}

function Nada(){
	transform.rotation.x=0;
	transform.rotation.z=0;
	GetComponent.<Animation>().Play (nada.name);
}

function Patrullar(){
	
	transform.rotation.x=0;
	transform.rotation.z=0;
	transform.Rotate (Vector3 ( 0,0,0)*Time.deltaTime); //25*direccion en la Y
	transform.Translate (Vector3(0,0,1)*Time.deltaTime);
	GetComponent.<Animation>().Play (caminar.name);
}

function Perseguir(){
	
	transform.rotation.x=0;
	transform.rotation.z=0;
	GetComponent.<Animation>().Play (correr.name);
	transform.LookAt(player);
	transform.Translate(Vector3(0,0,1*vel_seguir*Time.deltaTime));	
}

function Atacar(){
	transform.rotation.x=0;
	transform.rotation.z=0;
	GetComponent.<Animation>().Play (atacar.name);
	transform.Translate(Vector3(0,0,0.1*Time.deltaTime));

}

function Morir(){
	GetComponent.<Animation>().Play (morir.name);
}

function Esquivar(){
	GetComponent.<Animation>().Play (caminar.name);
}



