using UnityEngine;
using System.Collections;

/**
	Rango de Ataque del Citizen. Enténdase "ataque" como la recolección de comida, madera o metal y el volcado
	de los susodichos
 */
public class CitizenRDA : MonoBehaviour {

	public CitizenAI citizenAI;  
	public float woodResource;
	public Collider otherAux;
	public int attackState;


	public int counter;


	// Use this for initialization
	void Start () {	
		this.attackState = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (this.citizenAI.wood < 50) {
			this.attackState = 1;
		} else {
			this.attackState = 2;
		}
		*/
		switch (attackState) {
			case 1:Action();break;
			case 2:GoHome();break;
		}
	}

    void OnTriggerEnter( Collider other){
		//citizenAI.state = 0;
		//this.citizenAI.gameObject.GetComponent<Collider>().isTrigger=false; // desactivo el trigger para que no me pille otros Colliders
		if(other.gameObject.tag=="tree"){
			Resource resource = other.GetComponent<Resource>();
			GetComponent<CivilUnit>().StartHarvest(resource);

		}

	}

	void OnTriggerExit(Collider other){
		citizenAI.state = 2;

	}

	void Action(){
		citizenAI.gameObject.GetComponent<Collider>().isTrigger= false; 
		//otherAux.gameObject.GetComponent(Resource).capacity -= 5*Time.deltaTime;
		//this.citizenAI.getWood()+=5*Time.deltaTime;
	}
	void GoHome(){
		citizenAI.setTarget(citizenAI.citizenCenter);
		citizenAI.setState(2);
	}
}