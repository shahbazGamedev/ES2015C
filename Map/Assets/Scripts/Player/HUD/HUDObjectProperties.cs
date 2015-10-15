using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Shows the properties of the currently selected object in the HUD.
/// </summary>
public class HUDObjectProperties : HUDElement
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
    /// Updates the properties of the currently selected object in the HUD.
    /// </summary>
    void Update()
    {
        if (textComponent == null)
            return;

        // TODO: Find the properties of the selected unit and show them
        if (DisplayObject != null)
        {
            textComponent.text = 
                DisplayObject.GetType().ToString() + " property 1\n" +
                DisplayObject.GetType().ToString() + " property 2\n" +
                DisplayObject.GetType().ToString() + " property 3\n";
        }
        else
        {
            textComponent.text = "";
        }
    }
}
