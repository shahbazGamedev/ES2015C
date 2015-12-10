using UnityEngine;
using System.Collections;

public class VolumeControl : MonoBehaviour
{

	private GameObject menuVol;
	private bool activated = false;
	//public Player artificialIntelligence;
	//public PlayerCivilization civilitzation;

	// Use this for initialization
	//GameObject prefab;
	void Start () {
		GameObject menuVol;
		//artificialIntelligence = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        //civilitzation = GameObject.Find("EnemyPlayer1").GetComponent<Player>().civilization;
	}
	
	
	private void CreateMenuVC() {
        menuVol = Instantiate(Resources.Load("ControlVolumen")) as GameObject;     
    }

	void Update() {

		if (!activated) {
			if (Input.GetKey (KeyCode.V)) {
				activated=true;
				CreateMenuVC();

			}
		}

		else {
			if (Input.GetKey (KeyCode.V)) {
				activated=false;
				menuVol.active = false;
			}
		}
	}
	
}
