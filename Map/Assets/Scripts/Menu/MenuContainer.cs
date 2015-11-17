using UnityEngine;
using System.Linq;

/// <summary>
/// This script should be associated to the object that contains the different menu pages.
/// Controls which menu is currently shown and provides functions to change the selected menu.
/// </summary>
public class MenuContainer : MonoBehaviour
{
	void Start ()
    {
        // Make sure only the first page is enabled initially
        ChangePage(GetComponentsInChildren<MenuPage>(true).First());
	}
	
    /// <summary>
    /// Switch the menu to the given page.
    /// </summary>
    /// <param name="destinationPage">The menu page to switch to.</param>
	public void ChangePage(MenuPage destinationPage)
    {
        foreach (var page in GetComponentsInChildren<MenuPage>(true))
            page.gameObject.SetActive(page == destinationPage);
    }
}
