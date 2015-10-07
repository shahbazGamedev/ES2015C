#pragma strict

var velocidad:float = 5.0;

private var direccion:Vector3;

private var hit1:RaycastHit;
private var hit2:RaycastHit;
private var hit3:RaycastHit;
private var derecha45:Quaternion;
private var izquierda45:Quaternion;
private var controller:CharacterController;

function Start () {
	direccion = Vector3(Random.Range(-1.0,1.0),0.0,Random.Range(-1.0,1.0));
	derecha45 = Quaternion.Euler(0.0,45.0,0.0);
	izquierda45 = Quaternion.Euler(0.0,-45.0,0.0);
	controller = GetComponent(CharacterController);
}

function FixedUpdate () {
	var frente:Vector3 = direccion;
	var derecha:Vector3 = derecha45*direccion;
	var izquierda:Vector3 = izquierda45*direccion;
	if(!Physics.Raycast(transform.position,frente,hit1,20.0)) hit1.distance=20.0;
	if(!Physics.Raycast(transform.position,derecha,hit2,20.0)) hit2.distance=20.0;
	if(!Physics.Raycast(transform.position,izquierda,hit3,20.0)) hit3.distance=20.0;
	direccion = (hit1.distance*frente + hit2.distance*derecha + hit3.distance*izquierda) / 3.0; 
	direccion.Normalize();
	direccion.y=0.0;
	controller.SimpleMove(direccion*velocidad);	
}