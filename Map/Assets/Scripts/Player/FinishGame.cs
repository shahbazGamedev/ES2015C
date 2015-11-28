
using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour {
//Mirar tots els RTSobject que hi ha (2 enemics y un human), mirar el owner (enemic1, enemic2, human) que es quedin sense unitats

void Update(){
	switch(quienQueda()){
		case 1: // Solo has eliminado a un enemigo
			Debug.Log("Te queda un enemigo por eliminar");
			break;
		case 2: // Has ganado
			Debug.Log("Victorious");
			break;
		case 3: //Game over
			Debug.Log("Game over");
			break;
		case 0:
			break;
	}
}

int quienQueda(){
	int sumaE1, sumaE2, sumaH;
	sumaE1 = sumaE2 = sumaH = 0;
	foreach(var cosa in gameObject.GetComponentsinChildren<Unit>()){ //Miro todos los Unit y si hay alguno de owner lo sumo
		if(cosa.owner=="EnemyPlayer1"){
			sumaE1 += 1;
		}
		else if (cosa.owner=="EnemyPlayer2"){
			sumaE2 += 1;
		}
		else if(cosa.owner == "Player"){
			sumaH += 1;
		}
	}
	foreach(var town in GetComponentsinChildren<TownCenterBuilding>()){ //Miro todos los TownCenterBuilding y si hay alguno de owner lo sumo
		if(town.owner=="EnemyPlayer1"){
			sumaE1 += 1;
		}
		else if (town.owner=="EnemyPlayer2"){
			sumaE2 += 1;
		}
		else if(town.owner == "Player"){
			sumaH += 1;
		}
	}
	foreach(var atac in GetComponentsinChildren<ArmyBuilding>()){ //Miro todos los ArmyBuilding y si hay alguno de owner lo sumo
		if(atac.owner=="EnemyPlayer1"){
			sumaE1 += 1;
		}
		else if (atac.owner=="EnemyPlayer2"){
			sumaE2 += 1;
		}
		else if(atac.owner == "Player"){
			sumaH += 1;
		}
	}

	if(sumaE1 == 0 || sumaE2 == 0){ //Si no e sumado ningun enemigo de alguno de los dos, TE FALTA UN ENEMIGO POR ELIMINAR
		return 1;
	}
	else if(sumaE1 == 0 && sumaE2 == 0){ //Si no he sumado ningun enemigo, HAS GANADO
		return 2;
	}
	else if(sumaH == 0){ //Si no he sumado ningun "humano", HAS PERDIDO
		return 3;
	}
	return 0; // NADA
}


}