using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class CitizenAI : AI {
=======
public class CitizenAI : RTSObject {
>>>>>>> refs/remotes/origin/dev_TeamD
	
	RTSObject rtsObject;
	/*public AnimationClip idle;
	public AnimationClip walk;
	public AnimationClip run;
	public AnimationClip attack;
	public AnimationClip die;
	*/
<<<<<<< HEAD
=======
	public AIResources resources;
>>>>>>> refs/remotes/origin/dev_TeamD
	public int AIState; 		 
	public float MAX_RESOURCE = 50f;
	public float patrolTime = 0f; //contador de patrol/idle
	public bool patrol = true; //switch patrol/idle
	public float direction = 1f; //Sentido de giro mientras patrulla
	public Transform AITarget,auxAITarget;
	public Transform townCenter;
	public int speed = 3;
	public bool estoyLleno;
	public float distanceToObstacle;
	RaycastHit hit;
	Vector3 fwd;
	// Use this for initialization
	void Start () {
		//this.resources = new Transform[100];
		this.tag = "neutral";
		AIState = 0;
		townCenter = GameObject.Find ("Sumerian_TownCenter").transform;
		this.resources = new AIResources ();
		this.gameObject.GetComponent<CivilUnit> ().capacity = 0f;
	}
	// Update is called once per frame
	void Update () {

		switch (AIState) {
		case 0: Idle ();break;
		case 1: Walk ();break;
		case 2: Chase ();break;
		case 3: Collect ();break;
		case 4: GoHome();break;
		//case 5: Empty();break;
		}	
		estoyLleno = this.gameObject.GetComponent<CivilUnit> ().capacity >= MAX_RESOURCE;
		fwd = transform.position;
		if (Physics.SphereCast (fwd, 2, transform.forward, out hit, 8)) {
			Debug.Log ("HE DETECTADO UN OBJETO DE CLASE: " + hit.collider.tag);
			if (hit.collider.tag.Equals ("tree")) {
				this.tag = "woodCutter";
				this.AITarget = hit.collider.gameObject.transform; //Me quedo con la posicion del arbol
				this.AIState = 2; // Voy hacia el arbol
			}
			distanceToObstacle = hit.distance;
		}

		if (Physics.SphereCast (fwd, 0.5F, transform.forward, out hit, 1F)) {
			Debug.Log ("ESTOY JUNTO AL OBJETO DE CLASE: "+hit.collider.tag);
			if (hit.collider.tag.Equals ("townCenter")) {
				this.resources.wood += this.gameObject.GetComponent<CivilUnit> ().capacity; // Sumo al total de resource
				this.gameObject.GetComponent<CivilUnit> ().capacity = 0f; // elimino lo que llevo
				this.estoyLleno = false;
				this.AITarget=this.auxAITarget;
				this.AIState = 2;
			}else{

				AIState = 3; // Cambio mi estado a atacar
			}
		}


		if(healthPercentage<=0){ Die ();}
		
		if(patrolTime <= 0){
			this.patrolTime = 9; // Asi solo está parado 1 seg.
			this.patrol=false;
			AIState =0;
			//this.direction*=Random.Range(-1, 1);
			this.direction = 1;
		}
		if (patrolTime >= 10) {
			this.patrol = true;
			AIState = 1;
			//this.subState = 1; // walk to look for resource
		}
		if (patrol && AIState == 1){
			patrolTime-=1 * Time.deltaTime;
		}
		if (!patrol && AIState == 0){
			patrolTime+=1 * Time.deltaTime;
		}

		if (this.gameObject.GetComponent<CivilUnit> ().capacity >= MAX_RESOURCE) {
			this.AIState = 2;
		}

	}

	public void Idle(){
		//GetComponent<Animation>().Play(idle.name); 
	}
	
	public void Walk() {
		transform.LookAt(AITarget);

		this.gameObject.transform.Rotate(0,0*25*this.direction,0);//(0, 25*this.direction, 0) * Time.deltaTime); //25*direction en el eje de las Y
		this.gameObject.transform.Translate (0, 0, 1 * Time.deltaTime);
		//GetComponent<Animation>().Play(walk.name); 
	}

	public void Chase() {
		//GetComponent<Animation>().Play (run.name); 
		//Debug.Log ("AITarget: " + this.AITarget);
		transform.LookAt(this.AITarget);
		transform.Translate(0,0,1*speed*Time.deltaTime);	
	}
	
	public void Collect() {
		//GetComponent<Animation>().Play (attack.name); 
		this.gameObject.transform.Translate(0,0,0*1*Time.deltaTime);

		if (hit.collider.gameObject.tag == "tree") {
			auxAITarget = this.AITarget;
			if (this.gameObject.GetComponent<CivilUnit> ().capacity < this.MAX_RESOURCE) {
				this.gameObject.GetComponent<CivilUnit> ().capacity += 6f * Time.deltaTime; // cojo para mi
				hit.collider.gameObject.GetComponent<Resource> ().Remove (6f * Time.deltaTime); // resto cantidad del recurso
			}
		}

		if (this.gameObject.GetComponent<CivilUnit> ().capacity >= this.MAX_RESOURCE) {
			Debug.Log ("YA TENGO MAX_RESOURCE");
			this.AITarget = this.townCenter;
			this.AIState = 2;
		} 



	}

	public void GoHome(){
		this.AITarget = this.townCenter;
		this.AIState = 2;

	}

	public void Empty(){

		//AQUI VA UN IF
		//this.AIState = 1; // Si habia acabado el arbol, caminar para buscar otro.
		
	}
	public void Die(){Destroy (gameObject);}
}



