using UnityEngine;
using System.Collections;

public class CitizenAI : AI {
	/*public AnimationClip idle;*/
	//Resource aiResource;
	GameObject enemy1, closest;
	GameObject[] auxAIResourcesw,auxAIResourcesf,auxAIResourcesg,townCenters;
	public int AIState, speed = 5; 		 
	float patrolTime = 0f,direction = 1f,distanceToObstacle;
	bool patrol = true; //switch patrol/idle
	public Transform AITarget, townCenter;
	Transform aux, auxAITarget;
	RaycastHit hit;
	Vector3 fwd;
	float MAX_COLLECT = 50f;
	public bool estoyLleno;
	public bool estoyOcupado;
	AIResources resources;
	string resourceName;
	// Use this for initialization
	void Start () {
		this.tag = "civil";
		if(this.gameObject.GetComponent<RTSObject>().owner == GameObject.Find("EnemyPlayer1").GetComponent<Player>()){
			Debug.Log ("CITIZENAI DISABLED");
			GetComponent<CitizenAI>().enabled = false;
			return;
		}
		resources = new AIResources ();
		enemy1 = GameObject.Find("EnemyPlayer1");
		AIState = 0;
		townCenters = GameObject.FindGameObjectsWithTag ("townCenter");
		this.townCenter = ClosestTownCenter ().transform;
		this.hit = new RaycastHit ();
		//this.aiResource = new Resource ();
		this.estoyOcupado = false;
		this.estoyLleno = false;
		this.closest = new GameObject ();
		
	}
	// Update is called once per frame
	void Update () {
		
		switch (this.AIState) {
		case 0: Idle ();break;
		case 1: Walk ();break;
		case 2: Chase ();break;
		case 3: Collect();break;
		case 4: GoTownCenter();break;
		case 5: OnTownCenter();break;
		}	
		
		//this.estoyLleno = this.myWood >= MAX_WOOD;
		fwd = transform.position;
		
		// ---------------------------------- HE DETECTADO ALGO ---------------------------------------
		if (Physics.SphereCast (fwd, 20, transform.forward, out hit, 20)) {
			Debug.Log ("SOY "+this.tag +" Y HE DETECTADO EL OBJETO: " + hit.collider.name);
			
			// Si topo contra una madera
			if (hit.collider.tag.Equals ("wood")||hit.collider.tag.Equals("food")||hit.collider.tag.Equals("gold") && !estoyOcupado) {	
				// Si no existe en la lista
				if(!resources.resourcesArray.Contains(hit.collider.gameObject)){
					resources.resourcesArray.Add(hit.collider.gameObject); // Añado el recurso al array
				}
				this.AIState = 2;
				this.estoyOcupado = true;
				
				// Obtener el objeto de tipo Resource para los metodos de Logica
				//this.aiResource = hit.collider.gameObject.GetComponent<Resource> (); 
				
			}
			distanceToObstacle = hit.distance;
		}
		// --------------------------------- ESTOY JUNTO A ALGO ---------------------------------------
		if (Physics.SphereCast (fwd, 0F, transform.forward, out hit, 1F)) {
			//Debug.Log ("SOY "+this.tag +" Y ESTOY JUNTO AL OBJETO: "+hit.collider.name);
			if((hit.collider.tag.Equals ("wood") || hit.collider.tag.Equals("food")||hit.collider.tag.Equals("gold")) && !estoyLleno && estoyOcupado){
				this.auxAITarget = this.AITarget;
				this.AIState = 3;
			}
			if(hit.collider.tag.Equals ("townCenter")&& estoyLleno){
				this.AIState = 5;
			}
		}
		Debug.Log (this.AITarget);
		if (this.AITarget == null) {
			this.AIState = 1;
			this.estoyOcupado = false;
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
		resources.resourcesArray.Clear ();
		this.auxAIResourcesw = GameObject.FindGameObjectsWithTag("wood");
		this.auxAIResourcesf = GameObject.FindGameObjectsWithTag("food");
		this.auxAIResourcesg = GameObject.FindGameObjectsWithTag("gold");
		fullingResources ();
		this.AITarget = NextResource ();
		this.AIState = 2;
		//this.resourceName = this.AITarget.gameObject.name;
	}
	
	public void Walk() { // estado 1
		//GetComponent<Animation>().Play(walk.name); 
		transform.LookAt(AITarget);
		this.gameObject.transform.Rotate(0,(1*Random.Range(0,0)*this.direction)*Time.deltaTime,0);//(0, 25*this.direction, 0) * Time.deltaTime); //25*direction en el eje de las Y
		this.gameObject.transform.Translate (0, 0, 1 * speed * Time.deltaTime);
	}
	
	public void Chase() { // Estado 2
		//GetComponent<Animation>().Play (run.name); 
		//gameObject.GetComponent<CivilUnit> ().StartHarvest (aiResource); // Esto es para los metodos de Logica
		this.AITarget = NextResource ();
		transform.LookAt(this.AITarget);
		transform.Translate (0, 0, 1 * speed * Time.deltaTime);
	}
	
	public void Collect(){ // estado 3  
		
		if (hit.collider.gameObject.GetComponent<Resource>().isEmpty()) {
			resources.resourcesArray.Remove(AITarget.gameObject);
			this.auxAITarget = NextResource ();
			this.resourceName = this.AITarget.gameObject.name;
			this.AIState = 2;
		} else {
			this.AITarget = this.auxAITarget;
			this.resourceName = this.AITarget.gameObject.name;
			this.AIState = 2;
		}
		
		if (gameObject.GetComponent<CivilUnit>().collectionAmount <= MAX_COLLECT) {
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
			gameObject.GetComponent<CivilUnit>().collectionAmount += 11 * Time.deltaTime;
			
			if(hit.collider.GetComponent<Wood> ()!= null && hit.collider.gameObject.GetComponent<Wood> ().amountLeft <= 0) {
				this.AITarget = NextResource();
				this.AIState = 2;
			}
		} else {
			this.estoyLleno = true;
			this.AIState = 4;
		}
	}
	
	public void GoTownCenter(){ // Estado 4
		this.AITarget = this.townCenter;
		transform.LookAt (this.AITarget);
		this.AIState = 2;
	}
	
	public void OnTownCenter(){ // Estado 5
		
		switch (this.resourceName){
		case "wood":
			enemy1.GetComponent<Player>().initialWood += gameObject.GetComponent<Sumerian_civil> ().collectionAmount;
			break;
		case "food":
			enemy1.GetComponent<Player>().initialFood += gameObject.GetComponent<Sumerian_civil> ().collectionAmount;
			break;
		case "metal":
			enemy1.GetComponent<Player>().initialGold += gameObject.GetComponent<Sumerian_civil> ().collectionAmount;
			break;
		}
		
		gameObject.GetComponent<Sumerian_civil> ().collectionAmount = 0f;
		
		this.estoyLleno = false;
		this.AITarget = this.auxAITarget;
		this.resourceName = this.AITarget.gameObject.name;
		
		this.AIState = 2;
		
	}
	public void fullingResources(){
		Debug.Log ("------------------ RELLENO RESOURCES --------------------");
		for (int i =0; i<this.auxAIResourcesw.Length; i++) {
			if(!resources.resourcesArray.Contains(auxAIResourcesw[i])){
				resources.resourcesArray.Add(auxAIResourcesw[i]); // Añado el recurso al array
			}
		}
		Debug.Log ("TAMAÑO DEL ARRAY: " + resources.resourcesArray.Count);
		for (int i =0; i<this.auxAIResourcesf.Length; i++) {
			if(!resources.resourcesArray.Contains(auxAIResourcesf[i])){
				resources.resourcesArray.Add(auxAIResourcesf[i]); // Añado el recurso al array
			}
		}
		Debug.Log ("TAMAÑO DEL ARRAY: " + resources.resourcesArray.Count);
		for (int i =0; i<this.auxAIResourcesg.Length; i++) {
			if(!resources.resourcesArray.Contains(auxAIResourcesg[i])){
				resources.resourcesArray.Add(auxAIResourcesg[i]); // Añado el recurso al array
			}
		}
		Debug.Log ("TAMAÑO DEL ARRAY: " + resources.resourcesArray.Count);
		
	}
	
	public Transform NextResource(){
		Debug.Log ("OBTENIENDO EL SIGUIENTE RECURSO A RECOLECTAR");
		if (resources.resourcesArray.Count >= 1) {
			closest = Closest();
			return closest.transform;
		} else {
			this.AITarget = aux;
			this.auxAITarget = AITarget;
			this.AIState = 1;
			this.estoyOcupado = false;
			return null;
		}
	}
	
	public GameObject Closest(){
		Debug.Log ("BUSCANDO EL OBJECT MÁS CERCANO");
		GameObject aux;
		//closest.transform.position = new Vector3 (999,999,999);
		for(int i =0; i<resources.resourcesArray.Count;i++){
			aux = (GameObject)resources.resourcesArray[i];
			if((Vector3.Distance(aux.transform.position, transform.position)) < (Vector3.Distance(closest.transform.position, transform.position))){
				closest = aux;
			}
		}
		return closest;
	}
	/**
	 * Función que devuelve el townCenter más cercano. Se presupone que éste será el correspondiente al tipo de unidad que ejerce
	 * de Enemy.
	 */
	public GameObject ClosestTownCenter(){
		GameObject aux;
		for (int i=0; i<townCenters.Length; i++) {
			aux = (GameObject)townCenters[i];
			if((Vector3.Distance(aux.transform.position, transform.position)) < (Vector3.Distance(closest.transform.position, transform.position))){
				closest = aux;
			}
		}
		return closest;
	}
	
}


