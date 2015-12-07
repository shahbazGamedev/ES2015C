using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be associated to each menu panel.
/// </summary>
public class MenuMapContinue : MonoBehaviour
{
	public void ChangeToMapContinue(){
		Application.LoadLevel("map");
	}
}
