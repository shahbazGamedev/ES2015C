using UnityEngine;
using System.Collections;

public class CitizenAI : AI {

	public int AIState;  		 

	// Use this for initialization
	void Start () {
		this.tag = "civil";
		// Desconectamos el script CitizenAI.cs para el Jugador Player (humano)
		if(gameObject.GetComponent<RTSObject>().owner.human){
			//Debug.Log ("SOY "+gameObject.name+" HUMAN ESTA: "+GetComponent<RTSObject>().owner.human+" Y CITIZEN_AI ESTA DISABLED: ");
			GetComponent<CitizenAI>().enabled = false;
		}else{
			//Debug.Log ("SOY "+gameObject.name+" HUMAN ESTA: "+GetComponent<RTSObject>().owner.human+" Y CITIZEN_AI ESTA ENABLED: ");
			GetComponent<CitizenAI>().enabled = true;
		}
		gameObject.GetComponent<CivilUnit>().StartHarvest(null,"wood");
	}
	// Update is called once per frame
	void Update () {
		switch (this.AIState) {
		case 1:
			getWood ();
			break;
		case 2:
			getFood ();
			break;
		case 3:
			getGold ();
			break;
		}

	}

	public void getWood(){
		gameObject.GetComponent<CivilUnit> ().StartHarvest (null, "wood");
	}
	public void getFood(){
		gameObject.GetComponent<CivilUnit> ().StartHarvest (null, "food");
	}
	public void getGold(){
		gameObject.GetComponent<CivilUnit> ().StartHarvest (null, "gold");
	}
	

	
}


