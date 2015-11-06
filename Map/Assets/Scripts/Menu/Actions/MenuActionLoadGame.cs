using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Loads a previously saved game.
/// </summary>
public class MenuActionLoadGame : MonoBehaviour
{
    /// <summary>
    /// The dropdown that contains the list of saved games.
    /// </summary>
    public Dropdown savedGameDropdown;

    public void LoadGame()
    {
        // Check correct association
        if (savedGameDropdown == null)
        {
            Debug.LogError("Saved Game dropdown not associated.");
            return;
        }

        // Get selected slot no.
        Dropdown dropDownCampaign = savedGameDropdown.GetComponent<Dropdown>();
        int selectedSavedGame = dropDownCampaign.value;

        Debug.LogError("Load Game not implemented: Slot " + selectedSavedGame);
    }
}
