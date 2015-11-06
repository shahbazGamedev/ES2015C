using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Starts a new game.
/// </summary>
public class MenuActionStartGame : MonoBehaviour
{
    /// <summary>
    /// The dropdown that contains the list of campaigns.
    /// </summary>
    public Dropdown campaigsDropdown;

    public void StartGame()
    {
        // Check correct association
        if (campaigsDropdown == null)
        {
            Debug.LogError("Campaigns dropdown not associated.");
            return;
        }

        // All scenes after the main menu are the campaign: 1 is the first campaign, 2 is the second, etc.
        Application.LoadLevel(1+campaigsDropdown.value);
    }
}
