using UnityEngine;
using System.Collections;

public class CitizenAI : AI {
	/*public AnimationClip idle;*/
	Resource aiResource;
	public int AIState, speed = 3; 		 
	float patrolTime = 0f,direction = 1f,distanceToObstacle;
	bool patrol = true; //switch patrol/idle
	public Transform AITarget, townCenter;
	public Transform auxAITarget;
	RaycastHit hit;
	Vector3 fwd;
	float MAX_WOOD = 50f;
	public bool estoyLleno = false;
	public bool ocupado = false;
	public float myWood = 0f;
	// Use this for initialization
	void Start () {
		this.tag = "Untagged";
		AIState = 0;
		townCenter = GameObject.Find ("Sumerian_TownCenter").transform;
		this.hit = new RaycastHit ();
		this.aiResource = new Resource ();
		resources = new AIResources ();
	}
	// Update is called once per frame
	void Update () {

		switch (this.AIState) {
		case 0: Idle ();break;
		case 1: Walk ();break;
		
		}	
		//this.estoyLleno = this.myWood >= MAX_WOOD;
		fwd = transform.position;
		if (resources.resourcesArray.Count == 0) {
			this.AIState = 1;
		}
		// ---------------------------------- HE DETECTADO ALGO ---------------------------------------
		if (Physics.SphereCast (fwd, 1, transform.forward, out hit, 10)) {
			Debug.Log ("HE DETECTADO EL OBJETO: " + hit.collider.name);

			// Si topo contra una madera y el array de recursos solo tiene 0 o 1 objeto
			if (hit.collider.tag.Equals ("wood")) {	
				// Si no existe en la lista
				if(!resources.resourcesArray.Contains(hit.collider.gameObject.transform)){
					resources.resourcesArray.Add(hit.collider.gameObject.transform); // Añado el recurso al array
					Debug.Log("Ahora, el vector tiene: "+resources.resourcesArray.Count);
				}
				// Obtener el objeto de tipo Resource para los metodos de Logica
				//this.aiResource = hit.collider.gameObject.GetComponent<Resource> (); 
				this.tag = "woodCutter"; 
				this.AITarget = hit.collider.transform;
			}
			distanceToObstacle = hit.distance;
		}

		// Control de busqueda de recursos 
		// TODO: randomizarlo
		if (patrolTime <= 0){this.patrolTime = 9;this.patrol=false;AIState =0;this.direction = 1;}
		if (patrolTime >= 10) {this.patrol = true;AIState = 1;}
		if (patrol && AIState == 1){patrolTime-=1 * Time.deltaTime;}
		if (!patrol && AIState == 0){patrolTime+=1 * Time.deltaTime;}

	}

	public void Idle(){ // estado 0
		//GetComponent<Animation>().Play(idle.name); 
	}
	
	public void Walk() { // estado 1
		//GetComponent<Animation>().Play(walk.name); 
		Debug.Log ("Camino sin rumbo");
		transform.LookAt(AITarget);
		this.gameObject.transform.Rotate(0,(1*Random.Range(0,0)*this.direction)*Time.deltaTime,0);//(0, 25*this.direction, 0) * Time.deltaTime); //25*direction en el eje de las Y
		this.gameObject.transform.Translate (0, 0, 1 * Time.deltaTime);
	}

}


