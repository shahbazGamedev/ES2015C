
using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour {
//Mirar tots els RTSobject que hi ha (2 enemics y un human), mirar el owner (enemic1, enemic2, human) que es quedin sense unitats


void Update(){
	switch(quienQueda()){
		/*case 1: // Solo has eliminado a un enemigo
			Debug.Log("Te queda un enemigo por eliminar");
			break;*/
		case 1: // Has ganado
			Debug.Log("Victorious");
			break;
		case 2: //Game over
			Debug.Log("Game over");
			break;
		case 0:
			break;
	}
}

int quienQueda(){
	int sumaE1, sumaE2, sumaH;
	GameObject[] games;
	sumaE1 = sumaE2 = sumaH = 0;
	games = GameObject.FindGameObjectsWithTag("civil");
	foreach(var civil in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
		if(civil.GetComponent<CivilUnit>().owner.human){
			sumaH += 1;
		}
		else{
			sumaE1 += 1;
		}
	}
	games = GameObject.FindGameObjectsWithTag("mility");
	foreach(var mility in games){ //Miro todos los TownCenterBuilding y si hay alguno de owner lo sumo
		if(mility.GetComponent<Unit>().owner.human){
			sumaH += 1;
		}
		else{
			sumaE1 += 1;
		}
	}
	games = GameObject.FindGameObjectsWithTag("townCenter");
	foreach(var town in games){ //Miro todos los TownCenterBuilding y si hay alguno de owner lo sumo
		if(town.GetComponent<TownCenterBuilding>().owner.human){
			sumaH += 1;
		}
		else{
			sumaE1 += 1;
		}
	}
	games = GameObject.FindGameObjectsWithTag("armyBuilding");

	foreach(var army in games){ //Miro todos los TownCenterBuilding y si hay alguno de owner lo sumo
		if(army.GetComponent<ArmyBuilding>().owner.human){
			sumaH += 1;
		}
		else{
			sumaE1 += 1;
		}
	}

	if(sumaE1 == 0){ //Si no e sumado ningun enemigo HE GANADO
		return 1;
	}
	else if(sumaH == 0){ //Si no he sumado ningun "humano", HAS PERDIDO
		return 2;
	}
	return 0; // NADA
}


}