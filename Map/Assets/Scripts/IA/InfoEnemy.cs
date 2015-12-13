using UnityEngine;
using System.Collections;

public class InfoEnemy : MonoBehaviour
{

	private GameObject menu;
	private bool activated = false;
	public Player artificialIntelligence;
	public PlayerCivilization civilitzation;
	public bool menu_created = false;

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
				if (menu_created) {
					activated=true;
					menu.active = true;
				}
				else {
					activated=true;
					CreateMenu();
					menu_created = true; 
				}
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
