using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Behaviour to control the object health display in the HUD.
/// </summary>
public class HUDObjectHealthText : MonoBehaviour
{
    private RTSObject displayObject;

    void Start()
    {
        displayObject = null;

        UpdateObjectInformationInHud();
    }

    void Update()
    {
        UpdateObjectInformationInHud();
    }

    /// <summary>
    /// Set the object whose information is displayed in the HUD.
    /// </summary>
    /// <param name="displayObject">The object to display in the HUD.</param>
    public void SetDisplayObject(RTSObject newDisplayObject)
    {
        displayObject = newDisplayObject;
        UpdateObjectInformationInHud();
    }

    /// <summary>
    /// Updates the object health in the HUD.
    /// </summary>
    void UpdateObjectInformationInHud()
    {
        if (displayObject != null)
        {
            GetComponent<Text>().text = displayObject.hitPoints + "/" + displayObject.maxHitPoints;
        }
        else
        {
            GetComponent<Text>().text = "-";
        }
    }
}

