using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Controls which menu is currently shown and provides functions to change the selected menu.
/// </summary>
public class MenuSwitcher : MonoBehaviour
{
    List<GameObject> childMenuPanels;

	void Start ()
    {
        // Find the child objects, which are normally panels containing each menu
        childMenuPanels = new List<GameObject>();
        foreach (Transform child in transform)
            childMenuPanels.Add(child.gameObject);

        // Make sure only the main menu is enabled
        SelectMenu("PanelMainMenu");
	}
	
    /// <summary>
    /// Switch to the menu given by the panel with the specified name. 
    /// </summary>
    /// <param name="panelName"></param>
	public void SelectMenu(string panelName)
    {
        foreach (GameObject go in childMenuPanels)
            go.SetActive(go.name == panelName);
    }
}
