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
	VolumeControl volCont;

	public float auxvolumen;


	void Start () {

		ambiente = gameObject.AddComponent<AudioSource>();
		ambiente.clip = Resources.Load("Sounds/music1") as AudioClip;
		ambiente.volume = 0.5f;
		ambiente.Play();
		GameObject menuVol;


	}
	

	public void CreateMenuVC() {
        menuVol = Instantiate(Resources.Load("ControlVolumen")) as GameObject; 
		volCont = GameObject.Find("CanvasVolumen").GetComponent<VolumeControl>();
		auxvolumen = (float) volCont.ambiente2.volume;


    /*
    public void setAmbiente(AudioSource aux, int aaa) {
    	//VolumeControl vc = GetComponent<VolumeControl>();
		//xxx = vc.yyy;
        //ambiente.volume = aux.volume; 
        //xxx = aaa;
    }*/
    }
	public void Update() {



		if (!activated) {
			if (Input.GetKey (KeyCode.V)) {
				activated=true;
				if (menuVol == null) {
					CreateMenuVC ();
				}
				menuVol.active = true;

			}
		}

		else {
			auxvolumen = (float) volCont.ambiente2.volume;
			ambiente.volume = auxvolumen;
			if (Input.GetKey (KeyCode.V)) {
				activated=false;
				menuVol.active = false;
			}
		}
	}
	
}
