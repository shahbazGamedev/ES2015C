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
            //Application.LoadLevel("pauseGame");
        }else{
            //Application.LoadLevel("map");
            isPaused=true;
            Time.timeScale = 1;
        }
    }

    void OnGUI (){
        if(isPaused){
         
            // Si le bouton est présser alors isPaused devient faux donc le jeu reprend.
            if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 60, 100, 40), "Continuer")){
                isPaused = false;
            }
 
            // Si le bouton est présser alors on ferme completement le jeu ou charge la scene "Menu Principal
            // Dans le cas du bouton quitter il faut augmenter sa postion Y pour qu'il soit plus bas
            if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 00, 100, 40), "Main Menu")){
                // Application.Quit(); 
                Application.LoadLevel("menu");
            }
 
            if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 60, 100, 40), "Quitter")){
                Application.Quit(); 
                // Application.LoadLevel("CarBigParcour"); 
        	}
 
        }
    }
}