using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be associated to each menu panel.
/// </summary>
public class MenuFirst : MonoBehaviour
{
	public void ChangeToMenuFirst(){
		Application.LoadLevel("menu");
	}
}
