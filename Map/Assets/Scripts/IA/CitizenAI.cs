using UnityEngine;
using System.Collections;

public class CitizenAI : RTSObject {

	RTSObject rtsObject;

	public AnimationClip idle;
	public AnimationClip walk;
	public AnimationClip run;
	public AnimationClip attack;
	//public AnimationClip die;

	public string otherTag = "";
	public int state; 		 // Est
	//public int subState;
	//public int subSubState;
	public float MAX_RESOURCE = 50f;
	public float patrolTime = 0f; //contador de patrol/idle
	public bool patrol = true; //switch patrol/idle
	public float direction = 1f; //Sentido de giro mientras patrulla
	public Transform target;
	public Transform citizenCenter;
	public int speed = 3;
	public bool hasRol;
	public float distanceToObstacle;
	RaycastHit hit;
	Vector3 fwd;

	//CharacterController charCtrl;
	                        


	// Recursos que pot portar el Citizen
	//public float wood;
	//public float food;
	//public float metal;
	// ArrayList amb les localitzacions dels recursos que vaig trobant
	//public Transform[] resources;

	// Use this for initialization
	void Start () {
		//this.resources = new Transform[100];
		this.tag = "neutral";
		this.hasRol = false;
		this.state = 0;
		//this.rtsObject = this.gameObject.GetComponent (RTSObject);
		//this.charCtrl = GetComponent<CharacterController> ();
		this.distanceToObstacle = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		//this.gameObject.transform.rotation.x = 0;
		//this.gameObject.transform.rotation.z = 0;

		//Vector3 fwd = transform.TransformDirection (Vector3.forward);
		fwd = transform.position;// + charCtrl.center;
		//Debug.Log (fwd);
		if (Physics.SphereCast(fwd, 0.5f,transform.forward,out hit, 1)) {
			Debug.Log("TENGO EL OBJETO JUSTO ENFRENTE!!");
			this.state = 3;
			Resource resource = hit.collider.gameObject.GetComponent<Resource>();
			Debug.Log("------ Objeto:"+resource.gameObject.tag);
			distanceToObstacle = hit.distance;
			gameObject.GetComponent<CivilUnit>().StartHarvest(resource);

		}else

		if (Physics.SphereCast(fwd,5,transform.forward,out hit, 10)) {
			Debug.Log("HE DETECTADO UN OBJETO!!");
			this.setTarget(hit.collider.gameObject.transform);
			distanceToObstacle = hit.distance;
			this.state = 2;
			Debug.Log ("---- TAG DEL COLLIDER ----- "+hit.collider.tag);
			if(hit.collider.tag.Equals("tree")){
				this.tag = "woodCutter";			
				
			}

		}

		/* //DETECTA Y PERSIGUE CUANDO JUSTO ESTA EN FRENTE.

		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Debug.Log (fwd);
		if (Physics.Raycast(transform.position, fwd,out hit, 1)) {
			Debug.Log("TENGO EL OBJETO JUSTO ENFRENTE!!");
			this.state = 3;
		}else

		if (Physics.Raycast(transform.position,fwd,out hit,10)) {
			Debug.Log("HE DETECTADO UN OBJETO!!");
			this.setTarget(hit.collider.gameObject.transform);
			this.state = 2;
		}

		 */






		if(healthPercentage<=0){
			Die ();
		}

		if(patrolTime <= 0){
			this.patrolTime = 9; // Asi solo está parado 1 seg.
			this.patrol=false;
			this.state =0;
			//this.direction*=Random.Range(-1, 1);
			this.direction = 1;
		}
		if (patrolTime >= 10) {
			this.patrol = true;
			this.state = 1;
			//this.subState = 1; // walk to look for resource
		}
		if (patrol && this.state == 1){
			patrolTime-=1 * Time.deltaTime;
		}
		if (!patrol && this.state == 0){
			patrolTime+=1 * Time.deltaTime;
		}



		switch (this.state) {
		case 0: Idle ();break;
		case 1: Walk ();break;
		case 2: Chase ();break;
		case 3: Attack ();break;
		}

	}


    public void Idle(){
		//transform.rotation.x = 0;
		//transform.rotation.z = 0;
		//GetComponent<Animation>().Play(idle.name); 
	}

	public void Walk() {
		//this.gameObject.transform.rotation.x = 0;
		//this.gameObject.transform.rotation.z = 0;
		this.gameObject.transform.Rotate(0,0*25*this.direction,0);//(0, 25*this.direction, 0) * Time.deltaTime); //25*direction en el eje de las Y
		this.gameObject.transform.Translate (0, 0, 1 * Time.deltaTime);
		//GetComponent<Animation>().Play(walk.name); 
	}
	public void Chase() {
		//transform.rotation.x=0;
		//transform.rotation.z=0;

		//GetComponent<Animation>().Play (run.name); 
		transform.LookAt(target);
		transform.Translate(0,0,1*speed*Time.deltaTime);	
	}

	public void Attack() {
		//transform.rotation.x=0;
		//transform.rotation.z=0;
 		//GetComponent<Animation>().Play (attack.name); 
		this.gameObject.transform.Translate(0,0,0*1*Time.deltaTime);

		
	}

	public void Die(){
		Destroy (gameObject);
	}



	public void setState(int state){this.state = state;}
	public int getState(){return this.state;}

	//public void setSubState(int subState){this.subState = subState;}
	//public int getSubState(){return this.subState;}

	public void setTarget(Transform target){this.target = target;}
	public Transform getTarget(){return this.target;}

}



