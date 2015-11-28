/*using UnityEngine;
using System.Collections;

public class CitizenAI : AI {
	/*public AnimationClip idle;*/
	//Resource aiResource;
	/*public int AIState, speed = 8; 		 
	float patrolTime = 0f,direction = 1f,distanceToObstacle;
	bool patrol = true; //switch patrol/idle
	public Transform AITarget, townCenter;
	public Transform auxAITarget,aux;
	RaycastHit hit;
	Vector3 fwd;
	float MAX_COLLECT = 50f;
	public bool estoyLleno;
	public bool estoyOcupado;
	float myWood = 0f;
	// Use this for initialization
	void Start () {
		this.tag = "Untagged";
		AIState = 0;
		townCenter = GameObject.Find ("Sumerian_TownCenter").transform;
		this.hit = new RaycastHit ();
		this.aiResource = new Resource ();
		resources = new AIResources ();
		this.estoyOcupado = false;
		this.estoyLleno = false;
	}*/
	// Update is called once per frame
	/*void Update () {

		switch (this.AIState) {
		case 0: Idle ();break;
		case 1: Walk ();break;
		case 2: Chase ();break;
		case 3: Collect();break;
		case 4: GoTownCenter();break;
		case 5: OnTownCenter();break;
		}	*/

		//this.estoyLleno = this.myWood >= MAX_WOOD;
		/*fwd = transform.position;

		// ---------------------------------- HE DETECTADO ALGO ---------------------------------------
		/*if (Physics.SphereCast (fwd, 20, transform.forward, out hit, 20)) {
			//Debug.Log ("HE DETECTADO EL OBJETO: " + hit.collider.name);

			// Si topo contra una madera
			if (hit.collider.tag.Equals ("wood")||hit.collider.tag.Equals("food")||hit.collider.tag.Equals("gold")) {	
				// Si no existe en la lista
				if(!resources.resourcesArray.Contains(hit.collider.gameObject)){
					resources.resourcesArray.Add(hit.collider.gameObject); // Añado el recurso al array
				}
				// Obtener el objeto de tipo Resource para los metodos de Logica
				//this.aiResource = hit.collider.gameObject.GetComponent<Resource> (); 
				if(!estoyOcupado){
					this.AITarget = NextResource();
					this.AIState = 2;
					this.estoyOcupado = true;
				}
			}
			distanceToObstacle = hit.distance;
		}*/
		// --------------------------------- ESTOY JUNTO A ALGO ---------------------------------------
		/*if (Physics.SphereCast (fwd, 0F, transform.forward, out hit, 1F)) {
			//Debug.Log ("ESTOY JUNTO AL OBJETO: "+hit.collider.name);
			if((hit.collider.tag.Equals ("wood") || hit.collider.tag.Equals("food")||hit.collider.tag.Equals("gold")) && !estoyLleno && estoyOcupado){
				this.auxAITarget = this.AITarget;
				this.AIState = 3;
			}
			if(hit.collider.tag.Equals ("townCenter")&& estoyLleno){
				this.AIState = 5;
			}
		}
		if (this.AITarget == null) {this.AIState = 1;}
		// Control de busqueda de recursos 
		// TODO: randomizarlo
		if (patrolTime <= 0){this.patrolTime = 9;this.patrol=false;AIState =0;this.direction = 1;}
		if (patrolTime >= 10) {this.patrol = true;AIState = 1;}
		if (patrol && AIState == 1){patrolTime-=1 * Time.deltaTime;}
		if (!patrol && AIState == 0){patrolTime+=1 * Time.deltaTime;}

	}*/
	
	/*public void Idle(){ // estado 0
		//GetComponent<Animation>().Play(idle.name); 
	}*/
	
	/*public void Walk() { // estado 1
		//GetComponent<Animation>().Play(walk.name); 
		transform.LookAt(AITarget);
		this.gameObject.transform.Rotate(0,(1*Random.Range(0,0)*this.direction)*Time.deltaTime,0);//(0, 25*this.direction, 0) * Time.deltaTime); //25*direction en el eje de las Y
		this.gameObject.transform.Translate (0, 0, 1 * Time.deltaTime);
	}*/
	
	/*public void Chase() { // Estado 2
		//GetComponent<Animation>().Play (run.name); 
		//gameObject.GetComponent<CivilUnit> ().StartHarvest (aiResource); // Esto es para los metodos de Logica
		transform.LookAt(this.AITarget);
		transform.Translate (0, 0, 1 * speed * Time.deltaTime);
	}*/
	
	/*public void Collect(){ // estado 3              
		if (this.AITarget.gameObject.GetComponent<Resource>().isEmpty()) {
			resources.resourcesArray.Remove(AITarget.gameObject);
			this.auxAITarget = NextResource ();
			this.AIState = 2;
		} else {
			this.AITarget = this.auxAITarget;
			this.AIState = 2;
		}

		if (gameObject.GetComponent<Sumerian_civil>().collectionAmount <= MAX_COLLECT) {
			switch(hit.collider.tag){
			case "wood":
				hit.collider.gameObject.GetComponent<Wood> ().amountLeft -= 11 * Time.deltaTime;
				break;
			case "food":
				hit.collider.gameObject.GetComponent<Food> ().amountLeft -= 11 * Time.deltaTime;
				break;
			case "gold":
				hit.collider.gameObject.GetComponent<Gold> ().amountLeft -= 11 * Time.deltaTime;
				break;
			}
			gameObject.GetComponent<Sumerian_civil>().collectionAmount += 11 * Time.deltaTime;
		} else {
			this.estoyLleno = true;
			this.AIState = 4;
		}
	}*/
	
	/*public void GoTownCenter(){ // Estado 4
		this.AITarget = this.townCenter;
		transform.LookAt (this.AITarget);
		this.AIState = 2;
	}*/
	
	/*public void OnTownCenter(){ // Estado 5
		resources.wood += gameObject.GetComponent<Sumerian_civil> ().collectionAmount;
		this.myWood = 0f;
		gameObject.GetComponent<Sumerian_civil> ().collectionAmount = 0f;
		this.estoyLleno = false;
		this.AITarget = this.auxAITarget;
		this.AIState = 2;

	}*/
	
	/*public Transform NextResource(){
		GameObject objectAux;
		if (resources.resourcesArray.Count >= 1) {
			objectAux = (GameObject)resources.resourcesArray[0];
			aux=objectAux.transform;
			return aux;
		} else {
			this.AITarget = aux;
			this.AIState = 1;
			this.estoyOcupado = false;
			return null;
		}

	}

}*/


