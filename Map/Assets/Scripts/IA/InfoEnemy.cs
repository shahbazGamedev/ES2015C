using UnityEngine;
using System.Collections;

public class InfoEnemy : MonoBehaviour
{

	private GameObject menu;
	private bool activated = true;
	// Use this for initialization
	//GameObject prefab;
	void Start () {
		GameObject menu;
	}
	
	
	private void CreateMenu() {
        //menu = Instantiate(Resources.Load("HUDInfoEnemy")) as GameObject;     
    }

	void Update() {

		if (activated) {
			if (Input.GetKey (KeyCode.X)) {
				CreateMenu();
			}
		}

		else {
			if (Input.GetKey (KeyCode.X)) {
				menu.active = false;
			}
		}
	}
	/*
	public void OnDisable ()
	{
		if (guiObj != null)
			GameObject.DestroyImmediate (guiObj.gameObject);
	}	
*/
}
