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
    /// The dropdown that contains the list of human-available civilizations.
    /// </summary>
    public Dropdown humanCivilizationDropdown;

    /// <summary>
    /// The dropdown that contains the list of enemy-available civilizations.
    /// </summary>
    public Dropdown enemyCivilizationDropdown;

    /// <summary>
    /// The dropdown that contains the list of campaigns.
    /// </summary>
    public Dropdown campaigsDropdown;

    /// <summary>
    /// The object that keeps track of the menu game parameters when starting a game.
    /// </summary>
    public MenuGameParameters menuGameParametersObject;

    public void StartGame()
    {
        // Check correct association of dropdowns
        if (humanCivilizationDropdown == null)
        {
            Debug.LogErrorFormat("Human civilization dropdown not associated to {0}.", GetType());
            return;
        }

        if (enemyCivilizationDropdown == null)
        {
            Debug.LogErrorFormat("Enemy civilization dropdown not associated to {0}.", GetType());
            return;
        }

        if (campaigsDropdown == null)
        {
            Debug.LogErrorFormat("Campaigns dropdown not associated to {0}.", GetType());
            return;
        }

        if (menuGameParametersObject == null)
        {
            Debug.LogErrorFormat("Menu game parameters object not associated to {0}.", GetType());
            return;
        }

        // Convert the civilization choosen by the player to the internal PlayerCivilization enum
        // Instead of having a duplicate list, rely on the dropdown texts being the same as the
        // enum values, this avoids having to modify the script every time, but is not much flexible...
        var humanCivilizationText = humanCivilizationDropdown.options[humanCivilizationDropdown.value].text;
        var enemyCivilizationText = enemyCivilizationDropdown.options[enemyCivilizationDropdown.value].text;
        if (!Enum.IsDefined(typeof(PlayerCivilization), humanCivilizationText) ||
            !Enum.IsDefined(typeof(PlayerCivilization), enemyCivilizationText))
        {
            Debug.LogErrorFormat("Unknown civilization. Check out that the spelling on the " +
                "civilization dropdown matches that of the {0} enum.", typeof(PlayerCivilization));
            return;
        }

        // Set it in the menu game parameters object, so it can be passed between scenes
        menuGameParametersObject.SelectedHumanCivilization = (PlayerCivilization)
            Enum.Parse(typeof(PlayerCivilization), humanCivilizationText);
        menuGameParametersObject.SelectedEnemyCivilization = (PlayerCivilization)
                    Enum.Parse(typeof(PlayerCivilization), enemyCivilizationText);

        // All scenes after the main menu are the campaign: 1 is the first campaign, 2 is the second, etc.
        Application.LoadLevel(1+campaigsDropdown.value);
    }
}
