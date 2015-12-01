using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class CitizenAI : AI {
	RTSObject rtsObject;
	/*public AnimationClip idle;
	public AnimationClip walk;
	public AnimationClip run;
	public AnimationClip attack;
	public AnimationClip die;
	*/
	public AIResources resources;
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
	MovimientoAleatorioCivil movimiento;
	// Use this for initialization
	void Start () {

		//this.resources = new Transform[100];
		this.tag = "neutral";
		AIState = 0;
		this.movimiento = new MovimientoAleatorioCivil ();
		townCenter = GameObject.Find ("Sumerian_TownCenter").transform;
		this.resources = new AIResources ();
		hit = new RaycastHit ();
		this.gameObject.GetComponent<CivilUnit> ().collectionAmount = 0f;
	}
	// Update is called once per frame
	void Update () {

		estoyLleno = this.gameObject.GetComponent<CivilUnit> ().collectionAmount >= MAX_RESOURCE;
		switch (AIState) {
		case 0: Idle ();break;
		case 1: Walk ();break;
		case 2: Chase ();break;
		case 3: Collect ();break;
		case 4: GoHome();break;
		//case 5: Empty();break;
		}	

		fwd = transform.position;
		if (Physics.SphereCast (fwd, 1, transform.forward, out hit, 8)) {
			Debug.Log ("HE DETECTADO UN OBJETO DE CLASE: " + hit.collider.tag);
			if (hit.collider.tag.Equals ("tree")) {
				this.auxAITarget = this.AITarget; // me guardo la posicion del arbol para poder volver a el.
				this.tag = "woodCutter";
				this.AITarget = hit.collider.gameObject.transform; //Actualizo mi target.
				this.AIState = 2; // Voy hacia el arbol
			}
			distanceToObstacle = hit.distance;
		}

		if (Physics.SphereCast (fwd, 0F, transform.forward, out hit, 1F)) {
			Debug.Log ("ESTOY JUNTO AL OBJETO DE CLASE: "+hit.collider.tag);
			if (hit.collider.tag.Equals ("townCenter")) {
				//this.hit.collider.gameObject.GetComponent<AIResources>().wood += this.gameObject.GetComponent<CivilUnit> ().collectionAmount; // Sumo al total de resource
				this.gameObject.GetComponent<CivilUnit> ().collectionAmount = 0f; // elimino lo que llevo
				this.estoyLleno = false;
				this.AITarget=this.auxAITarget;
				this.AIState = 2;
			}
			else{

				AIState = 3; // Cambio mi estado a atacar
			}
		}


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

		if (this.gameObject.GetComponent<CivilUnit> ().collectionAmount >= MAX_RESOURCE) {
			this.AIState = 2;
		}

	}

	public void Idle(){
		//GetComponent<Animation>().Play(idle.name); 
	}
	
	public void Walk() {
		//this.movimiento.Invoke("NewHeading",10f);
		//GetComponent<MovimientoAleatorioCivil>().enabled = true;

		transform.LookAt(AITarget);
		this.gameObject.transform.Rotate(0,1*Random.Range(20f,30f)*this.direction,0);//(0, 25*this.direction, 0) * Time.deltaTime); //25*direction en el eje de las Y
		this.gameObject.transform.Translate (0, 0, 1 * Time.deltaTime);
		//GetComponent<Animation>().Play(walk.name); 

	}

	public void Chase() {
		//GetComponent<Animation>().Play (run.name); 
		//Debug.Log ("AITarget: " + this.AITarget);
		//GetComponent<MovimientoAleatorioCivil>().enabled = false;

		transform.LookAt(this.AITarget);
		transform.Translate(0,0,1*speed*Time.deltaTime);	
	}
	
	public void Collect() {
		//GetComponent<MovimientoAleatorioCivil>().enabled = false;
		//GetComponent<Animation>().Play (attack.name); 
		this.gameObject.transform.Translate(0,0,0*1*Time.deltaTime);
		if (hit.collider.tag.Equals("tree")) {
			this.auxAITarget = this.AITarget;
			Debug.Log("HE TOPADO CON UN ARBOL");
			if (this.gameObject.GetComponent<CivilUnit> ().collectionAmount < this.MAX_RESOURCE) {
				this.gameObject.GetComponent<CivilUnit> ().collectionAmount += 6f * Time.deltaTime; // cojo para mi
				hit.collider.gameObject.GetComponent<Resource> ().Remove (6f * Time.deltaTime); // resto cantidad del recurso
			}
		}

		if (this.gameObject.GetComponent<CivilUnit> ().collectionAmount >= this.MAX_RESOURCE) {
			Debug.Log ("YA TENGO MAX_RESOURCE");
			auxAITarget = this.AITarget;
			this.AITarget = this.townCenter;
			this.AIState = 2;
		} 
	}

	public void GoHome(){
		//GetComponent<MovimientoAleatorioCivil>().enabled = false;
		this.AITarget = this.townCenter;
		this.AIState = 2;

	}

	public void Empty(){

		//AQUI VA UN IF
		//this.AIState = 1; // Si habia acabado el arbol, caminar para buscar otro.
		
	}
	public void Die(){Destroy (gameObject);}
=======
public class CitizenAI : AI
{
    /*public AnimationClip idle;*/
    Resource aiResource;
    GameObject enemy1;
    public int AIState, speed = 5;
    float patrolTime = 0f, direction = 1f, distanceToObstacle;
    bool patrol = true; //switch patrol/idle
    public Transform AITarget, townCenter;
    public Transform auxAITarget, aux;
    RaycastHit hit;
    GameObject[] auxAIResourcesw, auxAIResourcesf, auxAIResourcesg;
    Vector3 fwd;
    float MAX_COLLECT = 50f;
    public bool estoyLleno;
    public bool estoyOcupado;
    float myWood = 0f;
    AIResources resources;
    // Use this for initialization
    void Start()
    {
        this.tag = "Untagged";
        if (GetComponent<RTSObject>().owner != GameObject.Find("EnemyPlayer1").GetComponent<Player>())
        {
            GetComponent<CitizenAI>().enabled = false;
        }
        else
        {
            resources = new AIResources();
            enemy1 = GameObject.Find("EnemyPlayer1");
            AIState = 0;
            townCenter = GameObject.Find("Sumerian_TownCenter").transform;
            this.hit = new RaycastHit();
            this.aiResource = new Resource();
            this.estoyOcupado = false;
            this.estoyLleno = false;
            this.AITarget = NextResource();
        }



    }
    // Update is called once per frame
    void Update()
    {

        switch (this.AIState)
        {
            case 0: Idle(); break;
            case 1: Walk(); break;
            case 2: Chase(); break;
            case 3: Collect(); break;
            case 4: GoTownCenter(); break;
            case 5: OnTownCenter(); break;
        }

        //this.estoyLleno = this.myWood >= MAX_WOOD;
        fwd = transform.position;

        // ---------------------------------- HE DETECTADO ALGO ---------------------------------------
        if (Physics.SphereCast(fwd, 20, transform.forward, out hit, 20))
        {
            Debug.Log("SOY " + this.tag + " Y HE DETECTADO EL OBJETO: " + hit.collider.name);

            // Si topo contra una recurso
            if (hit.collider.tag.Equals("wood") || hit.collider.tag.Equals("food") || hit.collider.tag.Equals("gold") && !estoyOcupado)
            {
                // Si no existe en la lista
                /*
				if(!resources.resourcesArray.Contains(hit.collider.gameObject)){
					resources.resourcesArray.Add(hit.collider.gameObject); // Añado el recurso al array
				}
				*/
                // Obtener el objeto de tipo Resource para los metodos de Logica
                //this.aiResource = hit.collider.gameObject.GetComponent<Resource> (); 
                if (!estoyOcupado)
                {
                    this.AITarget = NextResource();
                    this.AIState = 2;
                    this.estoyOcupado = true;
                }
            }
            distanceToObstacle = hit.distance;
        }
        // --------------------------------- ESTOY JUNTO A ALGO ---------------------------------------
        if (Physics.SphereCast(fwd, 0F, transform.forward, out hit, 1F))
        {
            Debug.Log("SOY " + this.tag + " Y ESTOY JUNTO AL OBJETO: " + hit.collider.name);
            if ((hit.collider.tag.Equals("wood") || hit.collider.tag.Equals("food") || hit.collider.tag.Equals("gold")) && !estoyLleno && estoyOcupado)
            {
                this.auxAITarget = this.AITarget;
                this.AIState = 3;
            }
            if (hit.collider.tag.Equals("townCenter") && estoyLleno)
            {
                this.AIState = 5;
            }
        }

        try
        {
            if (!resources.resourcesArray.Contains(this.auxAITarget.gameObject))
            {
                this.AITarget = NextResource();
                this.AIState = 2;
            }
        }
        catch (MissingReferenceException ex)
        {
            this.AITarget = NextResource();
            this.AIState = 2;
        }

        // Control de busqueda de recursos 
        // TODO: randomizarlo
        if (patrolTime <= 0) { this.patrolTime = 9; this.patrol = false; AIState = 0; this.direction = 1; }
        if (patrolTime >= 10) { this.patrol = true; AIState = 1; }
        if (patrol && AIState == 1) { patrolTime -= 1 * Time.deltaTime; }
        if (!patrol && AIState == 0) { patrolTime += 1 * Time.deltaTime; }

    }

    public void Idle()
    { // estado 0
      //GetComponent<Animation>().Play(idle.name); 
        this.auxAIResourcesw = GameObject.FindGameObjectsWithTag("wood");
        this.auxAIResourcesf = GameObject.FindGameObjectsWithTag("food");
        this.auxAIResourcesg = GameObject.FindGameObjectsWithTag("gold");
        fullingResources();
        this.AITarget = NextResource();
        this.auxAITarget = this.AITarget;
    }

    public void Walk()
    { // estado 1
      //GetComponent<Animation>().Play(walk.name); 
        transform.LookAt(AITarget);
        this.gameObject.transform.Rotate(0, (1 * Random.Range(0, 0) * this.direction) * Time.deltaTime, 0);//(0, 25*this.direction, 0) * Time.deltaTime); //25*direction en el eje de las Y
        this.gameObject.transform.Translate(0, 0, 1 * speed * Time.deltaTime);
    }

    public void Chase()
    { // Estado 2
      //GetComponent<Animation>().Play (run.name); 
      //gameObject.GetComponent<CivilUnit> ().StartHarvest (aiResource); // Esto es para los metodos de Logica
        transform.LookAt(this.AITarget);
        transform.Translate(0, 0, 1 * speed * Time.deltaTime);
    }

    public void Collect()
    { // estado 3              
        if (this.AITarget.gameObject.GetComponent<Resource>().isEmpty())
        {
            resources.resourcesArray.Remove(AITarget.gameObject);
            this.auxAITarget = NextResource();
            this.AIState = 2;
        }
        else
        {
            this.AITarget = this.auxAITarget;
            this.estoyOcupado = true;
            this.AIState = 2;
        }

        if (gameObject.GetComponent<Sumerian_civil>().collectionAmount <= MAX_COLLECT)
        {
            switch (hit.collider.tag)
            {
                case "wood":
                    hit.collider.gameObject.GetComponent<Wood>().amountLeft -= 11 * Time.deltaTime;
                    break;
                case "food":
                    hit.collider.gameObject.GetComponent<Food>().amountLeft -= 11 * Time.deltaTime;
                    break;
                case "gold":
                    hit.collider.gameObject.GetComponent<Gold>().amountLeft -= 11 * Time.deltaTime;
                    break;
            }
            gameObject.GetComponent<Sumerian_civil>().collectionAmount += 11 * Time.deltaTime;
        }
        else
        {
            this.estoyLleno = true;
            this.AIState = 4;
        }
    }

    public void GoTownCenter()
    { // Estado 4
        this.AITarget = this.townCenter;
        transform.LookAt(this.AITarget);
        this.AIState = 2;
    }

    public void OnTownCenter()
    { // Estado 5

        switch (this.auxAITarget.tag)
        {
            case "wood":
                enemy1.GetComponent<Player>().initialWood += gameObject.GetComponent<Sumerian_civil>().collectionAmount;
                break;
            case "food":
                enemy1.GetComponent<Player>().initialFood += gameObject.GetComponent<Sumerian_civil>().collectionAmount;
                break;
            case "metal":
                enemy1.GetComponent<Player>().initialGold += gameObject.GetComponent<Sumerian_civil>().collectionAmount;
                break;
        }

        gameObject.GetComponent<Sumerian_civil>().collectionAmount = 0f;

        this.estoyLleno = false;
        this.AITarget = this.auxAITarget;
        this.AIState = 2;

    }
    public void fullingResources()
    {
        Debug.Log("--------------------------- RELLENANDO RESOURCESARRAY -------------------------------");
        for (int i = 0; i < this.auxAIResourcesw.Length; i++)
        {
            if (!resources.resourcesArray.Contains(auxAIResourcesw[i]))
            {
                resources.resourcesArray.Add(auxAIResourcesw[i]); // Añado el recurso al array
            }
        }
        Debug.Log("NUMERO DE ELEMENTOS DE MADERA : " + resources.resourcesArray.Count);

        for (int i = 0; i < this.auxAIResourcesf.Length; i++)
        {
            if (!resources.resourcesArray.Contains(auxAIResourcesf[i]))
            {
                resources.resourcesArray.Add(auxAIResourcesf[i]); // Añado el recurso al array
            }
        }
        Debug.Log("NUMERO DE ELEMENTOS DE MADERA + COMIDA : " + resources.resourcesArray.Count);
        for (int i = 0; i < this.auxAIResourcesg.Length; i++)
        {
            if (!resources.resourcesArray.Contains(auxAIResourcesg[i]))
            {
                resources.resourcesArray.Add(auxAIResourcesg[i]); // Añado el recurso al array
            }
        }

        Debug.Log("NUMERO DE ELEMENTOS DE MADERA + COMIDA + ORO: " + resources.resourcesArray.Count);

    }

    public Transform NextResource()
    {
        Debug.Log("OBTENIENDO EL SIGUIENTE RECURSO A RECOLECTAR");
        GameObject closest;
        if (resources.resourcesArray.Count >= 1)
        {
            closest = Closest();
            aux = closest.transform;
            return aux;
        }
        else
        {
            this.AITarget = aux;
            this.auxAITarget = AITarget;
            this.AIState = 1;
            this.estoyOcupado = false;
            return null;
        }

    }

    public GameObject Closest()
    {
        Debug.Log("BUSCANDO EL OBJECT MÁS CERCANO");
        GameObject closest = new GameObject();
        GameObject aux;
        closest.transform.position = new Vector3(999, 999, 999);
        for (int i = 0; i < resources.resourcesArray.Count; i++)
        {
            aux = (GameObject)resources.resourcesArray[i];
            try
            {
                if ((Vector3.Distance(aux.transform.position, transform.position)) < (Vector3.Distance(closest.transform.position, transform.position)))
                {
                    closest = aux;
                }
            }
            catch (MissingReferenceException ex)
            {
                this.AITarget = NextResource();
                this.AIState = 2;

            }
        }
        Debug.Log("SOY " + this.tag + " Y MI PROXIMO OBJETIVO ES: " + closest.name);

        return closest;

    }

>>>>>>> dev_teamD5
}


