using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Behaviour to control the object name display in the HUD.
/// </summary>
public class HUDName : HUDElement
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
    /// Updates the object name in the HUD.
    /// </summary>
    protected override void UpdateObjectInformationInHud()
    {
        if (textComponent == null)
            return;

        if (DisplayObject != null)
        {
            textComponent.text = DisplayObject.objectName;
        }
        else
        {
            textComponent.text = "";
        }
    }
}
