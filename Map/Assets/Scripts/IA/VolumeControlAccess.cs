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
	public int xxx;

	void Start () {
		/*
		ambiente = gameObject.AddComponent<AudioSource>();
		ambiente.clip = Resources.Load("Sounds/music1") as AudioClip;
		ambiente.volume = 0.5f;
		ambiente.Play();
		GameObject menuVol;
		//artificialIntelligence = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        //civilitzation = GameObject.Find("EnemyPlayer1").GetComponent<Player>().civilization;*/

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
