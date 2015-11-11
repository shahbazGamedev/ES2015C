using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Starts a new game.
/// </summary>
public class MenuActionStartGame : MonoBehaviour
{
    /// <summary>
    /// The dropdown that contains the list of civilizations.
    /// </summary>
    public Dropdown civilizationDropdown;

    /// <summary>
    /// The dropdown that contains the list of campaigns.
    /// </summary>
    public Dropdown campaigsDropdown;

    public void StartGame()
    {
        // Check correct association of dropdowns
        if (civilizationDropdown == null)
        {
            Debug.LogErrorFormat("Civilization dropdown not associated to {0}.", GetType());
            return;
        }

        if (campaigsDropdown == null)
        {
            Debug.LogErrorFormat("Campaigns dropdown not associated to {0}.", GetType());
            return;
        }

        // Convert the civilization choosen by the player to the internal PlayerCivilization enum
        // Instead of having a duplicate list, rely on the dropdown texts being the same as the
        // enum values, this avoids having to modify the script every time, but is not much flexible...
        var civilizationText = civilizationDropdown.options[civilizationDropdown.value].text;
        if (!Enum.IsDefined(typeof(PlayerCivilization), civilizationText))
        {
            Debug.LogErrorFormat("Unknown civilization. Check out that the spelling on the " +
                "civilization dropdown matches that of the {0} enum.", typeof(PlayerCivilization));
            return;
        }

        var choosenCivilization = (PlayerCivilization)Enum.Parse(typeof(PlayerCivilization), civilizationText);

        // All scenes after the main menu are the campaign: 1 is the first campaign, 2 is the second, etc.
        Application.LoadLevel(1+campaigsDropdown.value);
    }
}
