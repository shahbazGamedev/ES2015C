using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be associated to each menu panel.
/// </summary>
public class MenuPause : MonoBehaviour
{
	private bool isPaused = true;

	public void ChangeMenuPause(){
		if(isPaused){
            isPaused=false;
            Time.timeScale = 0;
            Application.LoadLevel("pauseGame");
        }else{
            Application.LoadLevel("map");
            isPaused=true;
            Time.timeScale = 1;
        }
    }
}