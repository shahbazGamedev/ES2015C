using UnityEngine;
using System.Collections;

public class InfoEnemy : MonoBehaviour
{

	private GameObject menu;
	private bool activated = false;
	public Player artificialIntelligence;
	public PlayerCivilization civilitzation;

	// Use this for initialization
	//GameObject prefab;
	void Start () {
		GameObject menu;
		artificialIntelligence = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        civilitzation = GameObject.Find("EnemyPlayer1").GetComponent<Player>().civilization;
	}
	
	
	private void CreateMenu() {
        menu = Instantiate(Resources.Load("HUDEnemy")) as GameObject;     
    }

	void Update() {

		if (!activated) {
			if (Input.GetKey (KeyCode.X)) {
				activated=true;
				CreateMenu();

			}
		}

		else {
			if (Input.GetKey (KeyCode.X)) {
				activated=false;
				menu.active = false;
			}
		}
	}
	
}
