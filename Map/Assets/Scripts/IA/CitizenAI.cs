using UnityEngine;
using System.Collections;

public class CitizenAI : RTSObject {

	RTSObject rtsObject;

	public AnimationClip idle;
	public AnimationClip walk;
	public AnimationClip run;
	public AnimationClip attack;
	public AnimationClip die;

	public string otherTag = "";
	public int state; 		 // Est
	public int subState;
	public int subSubState;
	public float MAX_RESOURCE = 50f;
	public float pat = 0f; //contador de patrol/idle
	public bool patrol = true; //switch patrol/idle
	public float direction = 1f; //Sentido de giro mientras patrulla
	public Transform target;
	public Transform citizenCenter;
	public int speed = 3;
	public bool hasRol;

	// Recursos que pot portar el Citizen
	public float wood;
	public float food;
	public float metal;
	// ArrayList amb les localitzacions dels recursos que vaig trobant
	public Transform[] resources;

	// Use this for initialization
	void Start () {
		this.resources = new Transform[100];
		this.tag = "neutral";
		this.hasRol = false;
		//this.rtsObject = this.gameObject.GetComponent (RTSObject);
		                                             
	}

    /* CODI COMENTAT - NO COMPILA, SINTAXI INCORRECTA (MERGE 03/11/2015, COMENTAT PER JOAN BRUGUERA)

	// Update is called once per frame
	void Update () {
		//this.gameObject.transform.rotation.x = 0;
		//this.gameObject.transform.rotation.z = 0;
		if(this.resources.Length => his.resources.GetLength()-1){
			System.Array.Resize(ref this.resources, this.resources.GetLength()*2)
		}
		if(healthPercentage<=0){
			Die ();
		}

		if(pat <= 0){
			this.pat = 8; // Asi solo está parado 3 seg.
			this.patrol=false;
			this.state =0;
			this.direction*=Random.Range(-1, 1);
		}
		if (pat >= 10) {
			this.patrol = true;
			this.state = 1;
			this.subState = 1 // walk to look for resource
		}
		if (patrol && this.state == 1){
			pat-=1 * Time.deltaTime;
		}
		if (!patrol && this.state == 0){
			pat+=1 * Time.deltaTime;
		}


		if(
		// Control de estados
		switch(this.state){
			case 0:	// No hacer nada
				Idle(); 
				break;
			case 1:	// construir un Civil Center
				switch(this.subState){
					case 11: // Madera
						switch(this.subSubState){
							case 111: //buscar madera
								break;
							case 112: //ir al arbol
								break;
 							case 113: //recolectar madera del arbol
								break;
							case 114: //ir al deposito
								break;
							case 115: //volcar madera en el deposito
								break;
						}
						break;
					case 12: // Oro
						switch(this.subSubState){
							case 121: // buscar oro
								break;
							case 122: // ir a por la cantera
								break;
							case 123: // recolectar oro
								break;
							case 124: //ir al deposito
								break;
							case 125: //volcar oro en el deposito
					break;
						}
						
						break;
						
 					case 13:	//Comida
						switch(this.subSubState){
							case 131:// buscar comida
								break;
							case 132:// ir por la granja
								break;
							case 133: // recolectar comida
								break;
							case 134: //ir al deposito
								break;
							case 135: //volcar comida en el deposito
								break;
						}
						break;
				}

				break;
			case 2: // Serrería
				switch(this.subState){
					case 21: //Madera
						switch(this.subSubState){
							case 211: //buscar madera
								break;
							case 212: // ir hacia el arbol
								break;
							case 213: // recolectar madera
								break;
							case 214: //ir al deposito
								break;
							case 215: //volcar madera en el deposito
								break;
						}
						break;
					case 22: // Oro
						switch(this.subSubState){
							case 221: // buscar oro
								break;
							case 222: // ir hacia la cantera
								break;
							case 223:  // recolectar oro
								break;
							case 224: //ir al deposito
								break;
							case 225: //volcar oro en el deposito
								break;
						}
						
						break;
						
 					case 23:// Comida
						switch(this.subSubState){
							case 231://buscar comida
								break;
							case 232: // ir hacia la granja
								break;
							case 233: // recolectar comida.
								break;
							case 234: //ir al deposito
								break;
							case 235: //volcar comida en el deposito
								break;
						}
						break;
				}
			
				break;
			case 3: // Mina
				switch(this.subState){
					case 31: // Madera
						switch(this.subSubState){
							case 211: // buscar madera
								break;
							case 212: // ir a por el arbol
								break;
							case 213: // recolectar madera
								break;
							case 314: //ir al deposito
								break;
							case 315: //volcar madera en el deposito
								break;
						}
						break;
					case 32: //Oro
						switch(this.subSubState){
							case 321: // buscar oro
								break;
							case 322: // ir a por la cantera
								break;
							case 323: // recolectar oro
								break;
							case 324: //ir al deposito
								break;
							case 325: //volcar oro en el deposito
								break;
						}
						
						break;
						
					case 33: // Comida
						switch(this.subSubState){
							case 331: // buscar comida
								break;
							case 332: // ir por la granja
								break;
							case 333: // recolectar comida
								break;
							case 334: //ir al deposito
								break;
							case 335: //volcar comida en el deposito
								break;
						}
						break;
				}				
				break;

			case 4: // Molino
				switch(this.subState){
					case 41: // Madera
						switch(this.subSubState){
							case 411: // 
								break;
							case 412:
								break;
							case 413: 
								break;
							case 414: //ir al deposito
								break;
							case 415: //volcar madera en el deposito
								break;
							}
						break;
					case 42:
							switch(this.subSubState){
							case 421:
								break;
							case 422:
								break;
							case 423: 
								break;
							case 424: //ir al deposito
								break;
							case 425: //volcar oro en el deposito
								break;
							}
						
						break;
						
					case 43:
						switch(this.subSubState){
							case 431:
								break;
							case 432:
								break;
							case 433: 
								break;
							case 434: //ir al deposito
								break;
							case 435: //volcar comida en el deposito
								break;
							}

						break;
					}				
				break;
		}
	}
    */

    public void Idle(){
		//transform.rotation.x = 0;
		//transform.rotation.z = 0;
		GetComponent<Animation>().Play(idle.name); 
	}

	public void Walk(int subState) {
		//this.gameObject.transform.rotation.x = 0;
		//this.gameObject.transform.rotation.z = 0;
		this.gameObject.transform.Rotate(0,25*this.direction,0);//(0, 25*this.direction, 0) * Time.deltaTime); //25*direction en el eje de las Y
		this.gameObject.transform.Translate (0, 0, 1 * Time.deltaTime);
		GetComponent<Animation>().Play(walk.name); 
	}
	public void Chase(int subState) {
		//transform.rotation.x=0;
		//transform.rotation.z=0;

		GetComponent<Animation>().Play (run.name); 
		transform.LookAt(target);
		transform.Translate(0,0,1*speed*Time.deltaTime);	
	}

	public void Attack() {
		//transform.rotation.x=0;
		//transform.rotation.z=0;
 		GetComponent<Animation>().Play (attack.name); 
		this.gameObject.transform.Translate(0,0,1*Time.deltaTime);
		
	}

	public void Die(){
		GetComponent<Animation>().Play (die.name); 
	}

	public void setState(int state){this.state = state;}
	public int getState(){return this.state;}

	public void setSubState(int subState){this.subState = subState;}
	public int getSubState(){return this.subState;}

	public void setTarget(Transform target){this.target = target;}
	public Transform getTarget(){return this.target;}

}



