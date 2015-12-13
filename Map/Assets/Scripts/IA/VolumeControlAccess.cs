using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeControlAccess : MonoBehaviour
{

	private GameObject menuVol;
	private bool activated = false;
	//public Player artificialIntelligence;
	//public PlayerCivilization civilitzation;

	// Use this for initialization
	//GameObject prefab;

	public AudioSource ambiente;


	void Start () {


	}
	

	public void CreateMenuVC() {
        menuVol = Instantiate(Resources.Load("ControlVolumen")) as GameObject;  

    }
    /*
    public void setAmbiente(AudioSource aux, int aaa) {
    	//VolumeControl vc = GetComponent<VolumeControl>();
		//xxx = vc.yyy;
        //ambiente.volume = aux.volume; 
        //xxx = aaa;
    }*/

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
