using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Behaviour to control the object health display (as text) in the HUD.
/// </summary>
public class HUDHealthText : HUDElement
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
    /// Updates the object health in the HUD.
    /// </summary>
    void Update()
    {
        if (DisplayObject != null)
        {
            textComponent.text = DisplayObject.hitPoints + "/" + DisplayObject.maxHitPoints;
        }
        else
        {
            textComponent.text = "";
        }
    }
}

