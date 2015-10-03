using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDAction : HUDElement
{
    /// <summary>
    /// The index of the action in the object to display.
    /// </summary>
    public int ActionIndex;

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
    /// Updates the object action in the HUD.
    /// </summary>
    protected override void UpdateObjectInformationInHud()
    {
        if (textComponent == null)
            return;

        if (DisplayObject != null && ActionIndex < DisplayObject.actions.Length)
        {
            textComponent.text = DisplayObject.actions[ActionIndex];
        }
        else
        {
            textComponent.text = "";
        }
    }

    public override void HandleClick()
    {
        Debug.Log("click!");

        if (DisplayObject != null && ActionIndex < DisplayObject.actions.Length)
        {
            DisplayObject.PerformAction(DisplayObject.actions[ActionIndex]);
        }
    }
}
