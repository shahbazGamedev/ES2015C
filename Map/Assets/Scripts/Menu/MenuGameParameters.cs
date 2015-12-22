using UnityEngine;
using System.Collections;

/// <summary>
/// This script is associated to an empty object in the menu scene.
/// Its function is to pass parameters between the menu scene, and the game scenes.
/// In order to do this, this object will be configured in order not to be
/// destroyed when the scene changes, so the parameters are kept between scenes.
/// </summary>
public class MenuGameParameters : MonoBehaviour
{
    /// <summary>
    /// Get the civilization for the human player has been selected in the menu.
    /// </summary>
    public PlayerCivilization SelectedHumanCivilization
    {
        get;
        set;
    }

    /// <summary>
    /// Get the civilization that the enemy player that has been selected in the menu.
    /// </summary>
    public PlayerCivilization SelectedEnemyCivilization
    {
        get;
        set;
    }

    void Start ()
    {
        // Destroy previous instances of this object
        foreach (var mgp in GameObject.FindObjectsOfType<MenuGameParameters>())
        {
            if (mgp != this)
                Destroy(mgp.gameObject);
        }

        // Make sure this object is kept between scenes
        DontDestroyOnLoad(this);
	}
}
