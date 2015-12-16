using UnityEngine;
using System.Collections;

public class BuildSound: MonoBehaviour {
	public AudioClip shootSound;
	private AudioSource source;
	
	
	void Awake () {		
		source = GetComponent<AudioSource>();		
	}
	
	
	void Update () {
		source.PlayOneShot(shootSound,1);
			
	}
}
