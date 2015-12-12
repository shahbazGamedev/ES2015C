using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeControl : MonoBehaviour
{

	private GameObject menuVol;
	private bool activated = false;
	public bool is_mute = true;
	public float auxiliar = 0.5f;
	//public Player artificialIntelligence;
	//public PlayerCivilization civilitzation;

	// Use this for initialization
	//GameObject prefab;

	public AudioSource ambiente;

	public Sprite myImage;
	public Button myBtn;

	void Start () {
		ambiente = gameObject.AddComponent<AudioSource>();
		ambiente.clip = Resources.Load("Sounds/music1") as AudioClip;
		ambiente.volume = 0.0f;
		ambiente.Play();
		GameObject menuVol;
		myBtn = GameObject.Find("ButtonSound").GetComponent<Button>();;
		myImage = Resources.Load<Sprite>("mute"); // Make sure not to include the file extension
	    //Make sure it is added in the Inspector. Or reference it using GameObject.Find.
	    myBtn.image.sprite = myImage; // That is right, no need to GetComponent.
		
		//ambiente2.GetComponent.<VolumeControlAccess>().xxx = 2;
		//artificialIntelligence = GameObject.Find("EnemyPlayer1").GetComponent<Player>();
        //civilitzation = GameObject.Find("EnemyPlayer1").GetComponent<Player>().civilization;
	}
	
	public void PonVolumen (float valor) {

		ambiente.volume = valor/20f; //divido 20 porque hemos asginado que va entre 0 y 20
		
	}

	public void mute () {
		if (is_mute) {

	       	myImage = Resources.Load<Sprite>("mutenot"); // Make sure not to include the file extension
	        //Make sure it is added in the Inspector. Or reference it using GameObject.Find.
	        myBtn.image.sprite = myImage; // That is right, no need to GetComponent.


      		ambiente.volume = auxiliar;
      		is_mute = false;

		}
		else {
			myImage = Resources.Load<Sprite>("mute"); // Make sure not to include the file extension
	        //Make sure it is added in the Inspector. Or reference it using GameObject.Find.
	        myBtn.image.sprite = myImage; // That is right, no need to GetComponent.

			auxiliar = ambiente.volume;
			ambiente.volume = 0.0f;
      		//ambiente.mute = true;
      		is_mute = true;
		}
		
	}

	void Update() {

		
	}
}
