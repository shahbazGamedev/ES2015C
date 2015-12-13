using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Shows the available population in the HUD.
/// </summary>
public class HUDPopulation : HUDElement
{
	private Text textComponent;
	
	void Start()
	{
		textComponent = GetComponent<Text>();
		if (textComponent == null)
		{
			Debug.Log("Script of type " + GetType().Name + " without a Text component won't work.");
		}
	}
	
	/// <summary>
	/// Updates the quantity of the population in the HUD.
	/// </summary>
	void Update()
	{
		if (textComponent == null)
			return;
		
		// TODO: Find the quantity of the resource in the player and update it
		textComponent.text = Player.population + " / " + Player.maxPopulation;
	}
}
